$hostname = [System.Net.Dns]::GetHostName()
$configuration = "production"

if($hostname -eq "ubk3s")
{
        $configuration="ubk3s"
}

foreach($name in @(
        "BuildVersionsApi",
        "BuildVersions"
))
{
        $lowerName = $name.ToLower()
        $semanticVersion = "0.0.0.2"
        $description = "InitBuild"
        "Current build: ${env:REGISTRYHOST}/${lowerName}:${semanticVersion}"

        docker build -f ./${name}/Dockerfile --force-rm -t ${env:REGISTRYHOST}/${lowerName}:${semanticVersion} --build-arg Version="${semanticVersion}" -->
        docker push ${env:REGISTRYHOST}/${lowerName}:${semanticVersion}}
}



