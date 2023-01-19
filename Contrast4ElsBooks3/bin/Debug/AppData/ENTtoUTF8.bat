@rem @echo off

@rem set path="\\wip\S2TConv\jdk1.5.0_06\bin"
@rem set path="\\172.16.0.40\S2TConv\jdk1.5.0_06\bin"

if exist \\172.16.0.40\S2TConv\jdk1.5.0_06\bin (set path=\\172.16.0.40\S2TConv\jdk1.5.0_06\bin
goto doProcess)

if exist \\wip\S2TConv\jdk1.5.0_06\bin (set path=\\wip\S2TConv\jdk1.5.0_06\bin
goto doProcess)

:doProcess
REM copy %~n1.xml %~n1_org[ENT].xml

java -classpath "\\172.16.0.40\S2TConv\UTF8conversion" Replace %1 %2 \\172.16.0.40\S2TConv\UTF8conversion\Entities.ini


