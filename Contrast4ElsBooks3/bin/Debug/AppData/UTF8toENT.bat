@echo off

set path="D:\Deepak\Contrast4ElsBooks3\bin\Debug\jdk1.8.0_91\bin"

REM copy %~n1.xml %~n1_org[UTF].xml

java -classpath "D:\UTF8conversion" Utf82ent %1 %2 D:\UTF8conversion\utf.ini
