##################################################################################
## Copyright (c) Microsoft Corporation. All rights reserved.
##################################################################################
#This script is used to check if PowerShell Remoting is started on the given computer

param(
[string]$computerName = "." # host name or ip address
)

$triedCount = 0
$sutStatus = $null
while($triedCount -lt 300 -and $sutStatus -eq $null)
{
    $triedCount++
    $sutStatus = Test-WSMan $computerName -ErrorAction Ignore
}

if($sutStatus -ne $null)
{
    return $true
}
return $false


