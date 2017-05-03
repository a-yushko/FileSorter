@echo off
set target=Release
echo %date% %time%
..\DataGenerator\bin\%target%\DataGenerator.exe source.txt 1
..\DataSorter\bin\%target%\DataSorter.exe source.txt
..\DataGenerator\bin\%target%\DataGenerator.exe source10m.txt 10
..\DataSorter\bin\%target%\DataSorter.exe source10m.txt
..\DataGenerator\bin\%target%\DataGenerator.exe source100m.txt 100
..\DataSorter\bin\%target%\DataSorter.exe source100m.txt
..\DataGenerator\bin\%target%\DataGenerator.exe source1g.txt 1024
..\DataSorter\bin\%target%\DataSorter.exe source1g.txt
echo %date% %time%