foreach($name in @(
	"buildversions", 
	"buildversionsapi"
))
{
	#Increase this number manually if building several times
	$semanticVersion = "0.0.0.1"
	$description = "InitBuild"
	"Current build: ${name}:${semanticVersion}"

	docker build -f .\${name}\Dockerfile --force-rm -t ${name} --build-arg Version="${semanticVersion}" --build-arg Description="${description}" .
	docker tag ${name}:latest ${env:registryhost}/${name}:${semanticVersion}
	docker push $env:registryhost/${name}:$semanticVersion
}
