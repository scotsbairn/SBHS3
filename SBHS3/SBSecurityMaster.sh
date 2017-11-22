#!/bin/bash -f
rm -f SBSecurityMaster.vb
cat SBHS3/SBHS3/SBSecurityMaster.header | dos2unix >> SBSecurityMaster.vb
cat SBHS3/SBHS3/SBDevices.vb | dos2unix >> SBSecurityMaster.vb
cat SBHS3/SBHS3/SBHouse.vb | dos2unix >> SBSecurityMaster.vb
cat SBHS3/SBHS3/SBSecurity.vb | dos2unix >> SBSecurityMaster.vb
cat SBHS3/SBHS3/Skyview.vb | dos2unix >> SBSecurityMaster.vb
cat SBHS3/SBHS3/SBSecurityMaster.footer | dos2unix >> SBSecurityMaster.vb
