foreach($name in @(
	"buildversionsapi"
))
{
	$registryHost = "registry.local:5000"
	$kubeseal = "kubeseal"
	$semanticVersion = "0.0.0.1"
	"Current deploy: ${registryHost}/${name}:${semanticVersion}"

	cd ./deploy/${name}

	kustomize edit set image "${registryHost}/${name}:${semanticVersion}"

	if(Test-Path -Path ./secrets/*)
	{
		"Creating secrets"
		kubectl create secret generic ${name}-secret --output json --dry-run=client --from-file=./secrets |
			&${kubeseal} -n "${name}" --controller-namespace kube-system --format yaml > "secret.yaml"
	}

	cd ../..

	kubectl apply -k ./deploy/${name}

	#Restore secret.yaml and kustomization.yaml since this script alters them temporary
	if(Test-Path -Path ./deploy/${name}/secret.yaml)
	{
		git checkout ./deploy/${name}/secret.yaml
	}
	git checkout ./deploy/${name}/kustomization.yaml
}
