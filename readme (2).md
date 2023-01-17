## Prerequisites
* Powershell 7, Net Core based, from a powershell prompt run, *winget install --id Microsoft.Powershell --source winget*
* Chocolatey, install tool, *https://chocolatey.org*  
* ConEmu, command prompt, *choco install conemu*
* Docker Desktop for Windows, *choco install docker-desktop*  
* Curl, http command line tool, *choco install curl*  
* Kubernetes CLI, KubeCtl, *choco install kubernetes-cli*  
* Kustomize CLI, *choco install kustomize*  
* Kubernetes admin tool, K9s, *choco install k9s*  
* K3d, tool to run K3s Kubernetes in Docker, *choco install k3d*  

## Setting up Docker  
Download and install Docker for Windows Desktop. Configure it to use Windows Subsystem for Linux (WSL).  
## Setting up Kubernetes
To set up Kubernetes inside Docker we will use the command line tool named K3d. The command for creating a cluster is:  
*k3d cluster create*

## Database server    
In some of the examples we are using databases, for that purpose we are using MySql. How to deploy this will be showed further down in this document.  
## Event streaming server  
In some of the examples we are using event streaming, for that purpose we are using Nats. How to deploy this will be showed further down in this document.  
## Caching  
In some of the examples we are using caching, for that purpose we are using Redis. How to deploy this will be showed further down in this document.  

## Init cluster with MySql  
In the solutionfolder run following command:  
*kubectl apply -k ./deploy/mysql*
## Init cluster with Nats  
In the solutionfolder run following command:  
*kubectl apply -k ./deploy/nats*
## Init cluster with SealedSecrets  
In the solutionfolder run following command:  
*kubectl apply -k ./deploy/sealedsecrets*
## Init cluster with Prometheus  
In the solutionfolder run following command:  
*kubectl apply -k ./deploy/prometheus*
## Init cluster with Grafana  
In the solutionfolder run following command:  
*kubectl apply -k ./deploy/grafana*
## Init cluster with Redis  
In the solutionfolder run following command:  
*kubectl apply -k ./deploy/redis*
## Init cluster with CertManager  
In the solutionfolder run following command:  
*kubectl apply -k ./deploy/certmanager*

## BuildVersions NET 7 Backend

## BuildVersions Angular 14 Frontend
nvm install 16.18.1  
nvm use 16.18.1  

ng generate component BuildVersions --module=app --skip-tests  
ng generate component BuildVersionEdit --flat --module=app --skip-tests  
ng generate service ApiService --flat --module=app --skip-tests  


## Init cluster with BuildVersions and its BuildVersionsApi  
In the solutionfolder run following command:  
*./InitBuild.ps1*  
Follow that command with:  
*./InitDeploy.ps1*  

# TODO!
Make FE, BFF and BE run in the same namespace of the cluster!  
Add to Dockerfile:  
RUN apt-get update -y
RUN apt-get install -y iputils-ping dnsutils
