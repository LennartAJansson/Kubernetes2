function Show-Calendar {
param(
	[DateTime] $start = [DateTime]::Today,
	[DateTime] $end = $start,
	$firstDayOfWeek,
	[int[]] $highlightDay,
	[string[]] $highlightDate = [DateTime]::Today.ToString('yyyy-MM-dd')
	)

	#actual code for the function goes here see the end of the topic for the complete code sample
}

function K3s-Build
{
	param([string]$host, [string]$name, [string]$registrysource = $env:REGISTRYHOST, [string]$registryTarget = $env:REGISTRYHOSTX)

	$port=""
	if ($host -eq "local")
	{
		$port=":8080"
	}
	$configuration=$host
	$baseUrl = "http://buildversionsapi.${host}${port}"
	$curl = "curl"
	$lowerName = $name.ToLower()

	$alive = &${curl} -s "${baseUrl}/Ping" -H "accept: text/plain"
	if ($alive -ne "pong")
	{
		"You need to do an initial deploy of BuildVersionsApi"
		"Please run InitBuildVersion.ps1"
		return
	}

	$branch = git rev-parse --abbrev-ref HEAD
	$commit = git log -1 --pretty=format:"%H"
	$description = "${branch}: ${commit}"
	
	$buildVersion = &${curl} -s "${baseUrl}/buildversions/NewRevisionVersion/$name" | ConvertFrom-Json
	$semanticVersion = $buildVersion.semanticVersion

	if([string]::IsNullOrEmpty($semanticVersion) -or [string]::IsNullOrEmpty($description)) 
	{
		"Could not connect to git repo or BuildVersionsApi"
		"Please check that you are in the correct folder and that"
		"BuildVersionsApi is working correctly in your Kubernetes"
		return
	}

	"Current build source: ${registrySource}/${lowerName}:${semanticVersion}"
	"Current build target: ${registryTarget}/${lowerName}:${semanticVersion}"
	"Version: ${semanticVersion}"
	"Description: ${description}"

	docker build -f ./${name}/Dockerfile --force-rm -t ${registrySource}/${lowerName}:${semanticVersion} --build-arg Version="${semanticVersion}" --build-arg configuration="${configuration}" --build-arg Description="${description}" .
	docker tag "${registrySource}/${lowerName}:${semanticVersion}" "${registryTarget}/${lowerName}:${semanticVersion}"
	docker push "${registryTarget}/${lowerName}:${semanticVersion}"
}

