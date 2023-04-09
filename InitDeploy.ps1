$hostname = [System.Net.Dns]::GetHostName()

if($hostname -eq "ubk3s")
{
	$hostname=".${hostname}"
	$url = "http://buildversionapi.ubk3s"
}
else
{
	$hostname=""
	$url = "http://buildversionsapi.local:8080"
}

foreach($name in @(
	"BuildVersionsApi",
	"BuildVersions"
))
{
	$semanticVersion = "0.0.0.1"
	"Current deploy: ${env:REGISTRYHOST}/${name}:${semanticVersion}"
	
	if($hostname -eq "")
	{
		cd ./deploy/${name}
	}
	else
	{
		cd ./ubk3s/${name}
	}

	kustomize edit set image "${env:REGISTRYHOST}/${name}:${semanticVersion}"
	
	if(Test-Path -Path ./secrets/*)
	{
		"Creating secrets"
		if($hostname -eq "")
		{
			kubectl create secret generic ${name}-secret --output json --dry-run=client --from-file=./secrets |
				C:/Apps/kubeseal/kubeseal -n "${name}" --controller-namespace kube-system --format yaml > "secret.yaml"
			cd ../..
			kubectl apply -k ./deploy/${name}
		}
		else
		{
			kubectl create secret generic ${name}-secret --output json --dry-run=client --from-file=./secrets |
				kubeseal -n "${name}" --controller-namespace kube-system --format yaml > "secret.yaml"
			cd ../..
			kubectl apply -k ./ubk3s/${name}
		}
	}
	
	
	#Restore secret.yaml and kustomization.yaml since this script alters them temporary
	if([string]::IsNullOrEmpty($env:AGENT_NAME) -and [string]::IsNullOrEmtpy($hostname))
	{
		if(Test-Path -Path deploy/${name}/secret.yaml)
		{
			git checkout deploy/${name}/secret.yaml
		}
		git checkout deploy/${name}/kustomization.yaml
	}
}

$loop = 0
$alive = curl.exe -s "${url}/Ping" -H "accept: text/plain"

do
{
	$alive = curl.exe -s "${url}/Ping" -H "accept: text/plain"
	if($alive -ne "pong")
	{
		"Waiting 10 seconds for containers to start"
		start-sleep -s 10
		$loop++
	}
	else
	{
		$alive
	}
}
while($alive -ne "pong" -and $loop -lt 10)

if($alive -eq "pong")
{
	foreach($name in @(
		"buildversionsapi", 
		"buildversions" 
	))
	{
		$found = curl.exe -s "${url}/buildversions/GetVersionByName/${name}" -H 'Content-Type: application/json'
		if([string]::IsNullOrWhiteSpace($found))
		{
			"Adding ${name}"
			$bv = "{""projectName"": ""$name"",""major"": 0,""minor"": 0,""build"": 0,""revision"": 1,""semanticVersionText"": ""dev""}"
			curl.exe -X POST ${url}/buildversions/CreateProject -H 'Content-Type: application/json' -d $bv
		}
	}
}
else
{
	"Timed out waiting for a pong"
}