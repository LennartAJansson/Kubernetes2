$hostname = [System.Net.Dns]::GetHostName()
$port = "5000"
if($hostname -ne $env:computername)
{
  $hostname = ".ubk3s"
  $environment = "ubk3s"
}
else
{
  $hostname = ""
  $environment = "local"
}

k3d cluster create --config ./config.${environment}.yaml

$port = (docker port registry${hostname}).Split(':')[1]
$registryhost = "registry${hostname}:$($port)"
$env:registryhost=$registryhost

"Add following to C:\Windows\System32\drivers\etc\hosts (In linux /etc/hosts):"
"127.0.0.1 registry${hostname}"
"127.0.0.1 mysql${hostname}"
"127.0.0.1 mysql${hostname}.local"
"127.0.0.1 nats${hostname}"
"127.0.0.1 nats${hostname}.local"
"127.0.0.1 prometheus${hostname}"
"127.0.0.1 prometheus${hostname}.local"
"127.0.0.1 grafana${hostname}"
"127.0.0.1 grafana${hostname}.local"
"127.0.0.1 redis${hostname}"
"127.0.0.1 redis${hostname}.local"
"127.0.0.1 kafka${hostname}"
"127.0.0.1 kafka${hostname}.local"
""

# Verify that you have connection to your registry
"Trying to connect to registry:"
if($hostname -eq "")
{
	SETX /M REGISTRYHOST $registryhost
	curl.exe http://$registryhost/v2/_catalog
}
else
{
	$env:REGISTRYHOST=$registryhost
	curl http://$registryhost/v2/_catalog
}

"To remove everything regarding cluster, loadbalancer and registry:"
"k3d cluster delete k3s"


kubectl apply -k ./${environment}/mysql
kubectl apply -k ./${environment}/sealedsecrets
kubectl apply -k ./${environment}/nats
kubectl apply -k ./${environment}/redis
#kubectl apply -k ./${environment}/kafka
kubectl apply -k ./${environment}/prometheus
kubectl apply -k ./${environment}/grafana