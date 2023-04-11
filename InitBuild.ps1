$hostname = [System.Net.Dns]::GetHostName()

if($hostname -eq "ubk3s")
{
	$configuration="ubk3s"
}
else
{
	$configuration="production"
}

foreach($name in @(
	"BuildVersionsApi",
	"BuildVersions"
))
{
	$lowerName = $name.ToLower()
	$semanticVersion = "0.0.0.1"
	$description = "InitBuild"
	"Current build: ${env:REGISTRYHOST}/${lowerName}:${semanticVersion}"

	docker build -f ./${name}/Dockerfile --force-rm -t ${env:REGISTRYHOST}/${lowerName}:${semanticVersion} --build-arg Version="${semanticVersion}" --build-arg configuration="${configuration}" --build-arg Description="${description}" .
	docker push ${env:REGISTRYHOST}/${lowerName}:${semanticVersion}}
}