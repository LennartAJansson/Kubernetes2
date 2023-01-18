## How to shrink an existing installation
(when wsl file is getting to big)
This will remove K3s setup!
**Docker -> Troubleshoot -> Clean & Purge data -> All**  
In solution folder run AddKubernetesToDocker.ps1 
  
## How to restart Docker
(if switching between networks)  
**wsl --shutdown**  
When docker complains, choose to start up the wsl again  

## Update Angular:  
nvm install 16.18.1  
nvm use 16.18.1  

ng generate component BuildVersions --module=app --skip-tests  
ng generate component BuildVersionEdit --flat --module=app --skip-tests  
ng generate service ApiService --flat --module=app --skip-tests  

cd buildversions  
ng update --allow-dirty @angular/cdk @angular/cli @angular/core @angular/material  
cd ..  
cd countries  
ng update --allow-dirty @angular/cdk @angular/cli @angular/core @angular/material  
cd ..  
cd workloads  
ng update --allow-dirty @angular/cdk @angular/cli @angular/core @angular/material  
cd ..  
