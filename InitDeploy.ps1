foreach($name in @(
	"buildversions", 
	"buildversionsapi" 
))
{
	#Increase this number manually if building several times
	$semanticVersion = "0.0.0.1"
	"Current deploy: ${name}:${semanticVersion}"
	
	cd ./deploy/${name}
	#Set current semanticVersion for this deploy (modifies kustomization.yaml)
	kustomize edit set image "${env:registryhost}/${name}:${semanticVersion}"
	
	#Check if this deploy contains any secrets
	if(Test-Path -Path ./secrets/*)
	{
		"Creating secrets"
		#Set the new secrets (modifies secret.yaml)
		kubectl create secret generic ${name}-secret --output json --dry-run=client --from-file=./secrets |
			C:/Apps/kubeseal/kubeseal -n "${name}" --controller-namespace kube-system --format yaml > "secret.yaml"
	}
	
	cd ../..
	kubectl apply -k ./deploy/${name}
	
	#Restore secret.yaml and kustomization.yaml since this script alters them temporary
	if([string]::IsNullOrEmpty($env:AGENT_NAME))
	{
		if(Test-Path -Path deploy/${name}/secret.yaml)
		{
			git checkout deploy/${name}/secret.yaml
		}
		git checkout deploy/${name}/kustomization.yaml
	}

	#Add current built
	$bv = "{""projectName"": ""$name"",""major"": 0,""minor"": 0,""build"": 0,""revision"": 1,""semanticVersionText"": ""dev""}"
	curl.exe -X POST http://buildversionsapi.local:8081/buildversions/CreateProject -H 'Content-Type: application/json' -d $bv
}
