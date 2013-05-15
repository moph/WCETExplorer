@echo off
PATH=C:\Program Files (x86)\Microsoft Visual Studio 11.0\VC\bin;%PATH%
PATH=C:\Program Files (x86)\Microsoft Visual Studio 11.0\VC\bin\amd64;%PATH%
dumpbin.exe /all simulated_ES.dll /OUT:info.bin
