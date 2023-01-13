#!/perl/bin/perl -w
########################################################
#Usage: utf8ToEntity.pl PATH/filename.xml
#Project Name: LN Conversion 
#Developed By: Freelancer Ashutosh (ashu.prog@gmail.com)
#Version 1.0 (dated: 28-05-2022)
#Latest Update: v1.1 (dated: 29-05-2022)
########################################################
no warnings 'all';
$/ = undef;
use File::Path;
use Cwd qw(abs_path);

my $entBatPath = abs_path($0);
my ($inFile, $outFile) = "";
$inFile = $ARGV[0];
$outFile = $inFile;
$outFile=~s/\.xml\s*$/_out.xml/si;

open(JP, "javapath.ini");
my $javaPathTxt = <JP>;
close(JP);

my $javaPath = "java";
$javaPath = $1 if($javaPathTxt=~m/Java Path:\s*"([^"]+)"/si);

if($entBatPath=~m/\\/si)
{
	$entBatPath=~s/\\([^\\]+)\.exe/\\UTF8conversion\\/si;
	#$entBatPath=~s/\\([^\\]+)\.pl/\\UTF8conversion\\/si;
	$entBatPath=~s/\\$//si;
}
else
{
	$entBatPath=~s/\/([^\/]+)\.exe/\/UTF8conversion\//si;
	#$entBatPath=~s/\/([^\/]+)\.pl/\/UTF8conversion\//si;
	$entBatPath=~s/\/$//si;
}

system("\"$javaPath\" -classpath \"$entBatPath\" Utf82ent \"$inFile\" \"$inFile\" \"$entBatPath\\utf.ini\"");

open(OU, "$inFile");
my $outText = <OU>;
close(OU);

$outText=~s/\&iff\;/\&hArr\;/gs;
$outText=~s/\&ssetmn\;/\&setmn\;/gs;
$outText=~s/\&angst\;/\&Aring\;/gs;
$outText=~s/\&slarr\;/\&larr\;/gs;
$outText=~s/\&srarr\;/\&rarr\;/gs;
$outText=~s/\&rdquor\;/\&rdquo\;/gs;
$outText=~s/\&z\.hearts\;/\&hearts\;/gs;
$outText=~s/\&bottom\;/\&perp\;/gs;
$outText=~s/\&vprop\;/\&prop\;/gs;
$outText=~s/a\&\#x303\;/\&atilde\;/gs;
$outText=~s/a\&\#x308\;/\&auml\;/gs;
$outText=~s/a\&\#x30a\;/\&aring\;/gs;
$outText=~s/A\&\#x303\;/\&Atilde\;/gs;
$outText=~s/A\&\#x308\;/\&Auml\;/gs;
$outText=~s/A\&\#x30a\;/\&Aring\;/gs;
$outText=~s/C\&\#x327\;/\&Ccedil\;/gs;
$outText=~s/e\&\#x308\;/\&euml\;/gs;
$outText=~s/E\&\#x308\;/\&Euml\;/gs;
$outText=~s/i\&\#x308\;/\&iuml\;/gs;
$outText=~s/I\&\#x308\;/\&Iuml\;/gs;
$outText=~s/n\&\#x303\;/\&ntilde\;/gs;
$outText=~s/N\&\#x303\;/\&Ntilde\;/gs;
$outText=~s/o\&\#x303\;/\&otilde\;/gs;
$outText=~s/o\&\#x308\;/\&ouml\;/gs;
$outText=~s/O\&\#x303\;/\&Otilde\;/gs;
$outText=~s/O\&\#x308\;/\&Ouml\;/gs;
$outText=~s/s\&\#x30c\;/\&scaron\;/gs;
$outText=~s/S\&\#x30c\;/\&Scaron\;/gs;
$outText=~s/u\&\#x308\;/\&uuml\;/gs;
$outText=~s/U\&\#x308\;/\&Uuml\;/gs;
$outText=~s/y\&\#x308\;/\&yuml\;/gs;
$outText=~s/Y\&\#x308\;/\&Yuml\;/gs;
$outText=~s/z\&\#x30c\;/\&zcaron\;/gs;
$outText=~s/Z\&\#x30c\;/\&Zcaron\;/gs;

open(OUT, ">$inFile");
print OUT $outText;
close(OUT);


