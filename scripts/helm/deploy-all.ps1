Param(
    [parameter(Mandatory=$true)][string]$resourceGroup,
    [parameter(Mandatory=$false)][string]$baseName="aksws",
    [parameter(Mandatory=$false)][string]$imageTag="latest",
    [parameter(Mandatory=$false)][bool]$dryrun=$false
)

$aks = $(az aks list -g $resourceGroup --query "[0]") | ConvertFrom-Json

if ($null -eq $aks) {
    Write-Host "AKS not found in RG: $resourceGroup" -ForegroundColor Red
    exit 1
}
else {
    $aksName=$aks.name
    Write-Host "Found AKS $aksName" -ForegroundColor Yellow
}


$acr = $(az acr list -g $resourceGroup --query "[0]") | ConvertFrom-Json

if ($null -eq $acr) {
    Write-Host "ACR not found in RG: $resourceGroup" -ForegroundColor Red
    exit 1
}
else {
    $acrName = $acr.name
    Write-Host "Found ACR $acrName" -ForegroundColor Yellow
}

$hostName = $aks.addonProfiles.httpApplicationRouting.config.HTTPApplicationRoutingZoneName
$acrLoginServer=$acr.loginServer

Write-Host "Running charts" -ForegroundColor Green

$debugcmd = ""
if ($dryrun) {
    $debugcmd = "--dry-run --debug"
}

cmd /c "helm install -f gvalues.yaml -n $baseName-catalog $debugcmd --set ingress.hosts={$hostName} --set ingress.tls[0].hosts={$hostName} --set image.repository=$acrLoginServer/catalog --set image.tag=$imageTag  catalog-api"
cmd /c "helm install -f gvalues.yaml -n $baseName-basket $debugcmd --set ingress.hosts={$hostName} --set ingress.tls[0].hosts={$hostName} --set image.repository=$acrLoginServer/basket --set image.tag=$imageTag basket-api"
cmd /c "helm install -f gvalues.yaml -n $baseName-order  $debugcmd --set ingress.hosts={$hostName} --set ingress.tls[0].hosts={$hostName} --set image.repository=$acrLoginServer/order --set image.tag=$imageTag order-api"
cmd /c "helm install -f gvalues.yaml -n $baseName-website  $debugcmd --set ingress.hosts={$hostName} --set ingress.tls[0].hosts={$hostName} --set image.repository=$acrLoginServer/website --set image.tag=$imageTag website"

Write-Host "Charts deployed" -ForegroundColor Green