## Setup cluster with BuildVersionsApi  
## BuildVersions NET 7 Backend

## BuildVersions Angular 14 Frontend

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
