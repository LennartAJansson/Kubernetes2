foreach($name in @(
	"BuildVersionsApi"
))
{
	$registryHost = "registry.local:5000"
	$semanticVersion = "0.0.0.1"
	$configuration = "production"

	$lowerName = $name.ToLower()
	$branch = git rev-parse --abbrev-ref HEAD
	$commit = git log -1 --pretty=format:"%H"
	$description = "${branch}: ${commit}"

	"Current build: ${registryHost}/${lowerName}:${semanticVersion}"

	docker build -f ./${name}/Dockerfile --force-rm -t ${registryHost}/${lowerName}:${semanticVersion} --build-arg Version="${semanticVersion}" --build-arg configuration="${configuration}" --build-arg Description="${description}" .
	docker push ${registryHost}/${lowerName}:${semanticVersion}
}