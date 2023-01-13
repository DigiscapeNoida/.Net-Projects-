<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:xs="http://www.w3.org/2001/XMLSchema"
    xmlns:mf="http://example.com/mf"
    exclude-result-prefixes="xs mf"
    version="2.0">
    
    <!--<xsl:character-map name="SA">
        <xsl:output-character character="↶" string="&amp;cularr;"/>
        <xsl:output-character character="↷" string="&amp;curarr;"/>
        <xsl:output-character character="⇓" string="&amp;dArr;"/>
        <xsl:output-character character="⇃" string="&amp;dharl;"/>
        <xsl:output-character character="⇂" string="&amp;dharr;"/>
        <xsl:output-character character="⇚" string="&amp;lAarr;"/>
        <xsl:output-character character="↞" string="&amp;Larr;"/>
        <xsl:output-character character="↩" string="&amp;larrhk;"/>
        <xsl:output-character character="↫" string="&amp;larrlp;"/>
        <xsl:output-character character="↢" string="&amp;larrtl;"/>
        <xsl:output-character character="↽" string="&amp;lhard;"/>
        <xsl:output-character character="↼" string="&amp;lharu;"/>
        <xsl:output-character character="⇔" string="&amp;hArr;"/>
        <xsl:output-character character="↔" string="&amp;harr;"/>
        <xsl:output-character character="↭" string="&amp;harrw;"/>
        <xsl:output-character character="↰" string="&amp;lsh;"/>
        <xsl:output-character character="↦" string="&amp;map;"/>
        <xsl:output-character character="⊸" string="&amp;mumap;"/>
        <xsl:output-character character="↗" string="&amp;nearr;"/>
        <xsl:output-character character="⇍" string="&amp;nlArr;"/>
        <xsl:output-character character="↚" string="&amp;nlarr;"/>
        <xsl:output-character character="⇎" string="&amp;nhArr;"/>
        <xsl:output-character character="↮" string="&amp;nharr;"/>
        <xsl:output-character character="↛" string="&amp;nrarr;"/>
        <xsl:output-character character="⇏" string="&amp;nrArr;"/>
        <xsl:output-character character="↖" string="&amp;nwarr;"/>
        <xsl:output-character character="↺" string="&amp;olarr;"/>
        <xsl:output-character character="↻" string="&amp;orarr;"/>
        <xsl:output-character character="⇛" string="&amp;rAarr;"/>
        <xsl:output-character character="↠" string="&amp;Rarr;"/>
        <xsl:output-character character="↪" string="&amp;rarrhk;"/>
        <xsl:output-character character="↬" string="&amp;rarrlp;"/>
        <xsl:output-character character="↣" string="&amp;rarrtl;"/>
        <xsl:output-character character="↝" string="&amp;rarrw;"/>
        <xsl:output-character character="⇁" string="&amp;rhard;"/>
        <xsl:output-character character="⇀" string="&amp;rharu;"/>
        <xsl:output-character character="↱" string="&amp;rsh;"/>
        <xsl:output-character character="⇑" string="&amp;uArr;"/>
        <xsl:output-character character="⇕" string="&amp;vArr;"/>
        <xsl:output-character character="↕" string="&amp;varr;"/>
        <xsl:output-character character="↿" string="&amp;uharl;"/>
        <xsl:output-character character="↾" string="&amp;uharr;"/>
        <xsl:output-character character="⟸" string="&amp;xlArr;"/>
        <xsl:output-character character="⟺" string="&amp;xhArr;"/>
        <xsl:output-character character="⟷" string="&amp;xharr;"/>
        <xsl:output-character character="⟹" string="&amp;xrArr;"/>
        <xsl:output-character character="⨿" string="&amp;amalg;"/>
        <xsl:output-character character="⌆" string="&amp;Barwed;"/>
        <xsl:output-character character="⌅" string="&amp;barwed;"/>
        <xsl:output-character character="⋒" string="&amp;Cap;"/>
        <xsl:output-character character="⋓" string="&amp;Cup;"/>
        <xsl:output-character character="⋎" string="&amp;cuvee;"/>
        <xsl:output-character character="⋏" string="&amp;cuwed;"/>
        <xsl:output-character character="⋄" string="&amp;diam;"/>
        <xsl:output-character character="⋇" string="&amp;divonx;"/>
        <xsl:output-character character="⊺" string="&amp;intcal;"/>
        <xsl:output-character character="⋋" string="&amp;lthree;"/>
        <xsl:output-character character="⋉" string="&amp;ltimes;"/>
        <xsl:output-character character="⊟" string="&amp;minusb;"/>
        <xsl:output-character character="⊛" string="&amp;oast;"/>
        <xsl:output-character character="⊚" string="&amp;ocir;"/>
        <xsl:output-character character="⊝" string="&amp;odash;"/>
        <xsl:output-character character="⊙" string="&amp;odot;"/>
        <xsl:output-character character="⊖" string="&amp;ominus;"/>
        <xsl:output-character character="⊕" string="&amp;oplus;"/>
        <xsl:output-character character="⊘" string="&amp;osol;"/>
        <xsl:output-character character="⊗" string="&amp;otimes;"/>
        <xsl:output-character character="⊞" string="&amp;plusb;"/>
        <xsl:output-character character="∔" string="&amp;plusdo;"/>
        <xsl:output-character character="⋌" string="&amp;rthree;"/>
        <xsl:output-character character="⋊" string="&amp;rtimes;"/>
        <xsl:output-character character="⋅" string="&amp;sdot;"/>
        <xsl:output-character character="⊡" string="&amp;sdotb;"/>
        <xsl:output-character character="∖" string="&amp;setmn;"/>
        <xsl:output-character character="⊓" string="&amp;sqcap;"/>
        <xsl:output-character character="⊔" string="&amp;sqcup;"/>
        <xsl:output-character character="∖" string="&amp;ssetmn;"/>
        <xsl:output-character character="⋆" string="&amp;sstarf;"/>
        <xsl:output-character character="⊠" string="&amp;timesb;"/>
        <xsl:output-character character="⊤" string="&amp;top;"/>
        <xsl:output-character character="⊎" string="&amp;uplus;"/>
        <xsl:output-character character="≀" string="&amp;wreath;"/>
        <xsl:output-character character="◯" string="&amp;xcirc;"/>
        <xsl:output-character character="▽" string="&amp;xdtri;"/>
        <xsl:output-character character="△" string="&amp;xutri;"/>
        <xsl:output-character character="∐" string="&amp;coprod;"/>
        <xsl:output-character character="∏" string="&amp;prod;"/>
        <xsl:output-character character="∑" string="&amp;sum;"/>
        <xsl:output-character character="а" string="&amp;acy;"/>
        <xsl:output-character character="А" string="&amp;Acy;"/>
        <xsl:output-character character="б" string="&amp;bcy;"/>
        <xsl:output-character character="Б" string="&amp;Bcy;"/>
        <xsl:output-character character="в" string="&amp;vcy;"/>
        <xsl:output-character character="В" string="&amp;Vcy;"/>
        <xsl:output-character character="г" string="&amp;gcy;"/>
        <xsl:output-character character="Г" string="&amp;Gcy;"/>
        <xsl:output-character character="д" string="&amp;dcy;"/>
        <xsl:output-character character="Д" string="&amp;Dcy;"/>
        <xsl:output-character character="е" string="&amp;iecy;"/>
        <xsl:output-character character="Е" string="&amp;IEcy;"/>
        <xsl:output-character character="ё" string="&amp;iocy;"/>
        <xsl:output-character character="Ё" string="&amp;IOcy;"/>
        <xsl:output-character character="ж" string="&amp;zhcy;"/>
        <xsl:output-character character="Ж" string="&amp;ZHcy;"/>
        <xsl:output-character character="з" string="&amp;zcy;"/>
        <xsl:output-character character="З" string="&amp;Zcy;"/>
        <xsl:output-character character="и" string="&amp;icy;"/>
        <xsl:output-character character="И" string="&amp;Icy;"/>
        <xsl:output-character character="й" string="&amp;jcy;"/>
        <xsl:output-character character="Й" string="&amp;Jcy;"/>
        <xsl:output-character character="к" string="&amp;kcy;"/>
        <xsl:output-character character="К" string="&amp;Kcy;"/>
        <xsl:output-character character="л" string="&amp;lcy;"/>
        <xsl:output-character character="Л" string="&amp;Lcy;"/>
        <xsl:output-character character="м" string="&amp;mcy;"/>
        <xsl:output-character character="М" string="&amp;Mcy;"/>
        <xsl:output-character character="н" string="&amp;ncy;"/>
        <xsl:output-character character="Н" string="&amp;Ncy;"/>
        <xsl:output-character character="о" string="&amp;ocy;"/>
        <xsl:output-character character="О" string="&amp;Ocy;"/>
        <xsl:output-character character="п" string="&amp;pcy;"/>
        <xsl:output-character character="П" string="&amp;Pcy;"/>
        <xsl:output-character character="р" string="&amp;rcy;"/>
        <xsl:output-character character="Р" string="&amp;Rcy;"/>
        <xsl:output-character character="с" string="&amp;scy;"/>
        <xsl:output-character character="С" string="&amp;Scy;"/>
        <xsl:output-character character="т" string="&amp;tcy;"/>
        <xsl:output-character character="Т" string="&amp;Tcy;"/>
        <xsl:output-character character="у" string="&amp;ucy;"/>
        <xsl:output-character character="У" string="&amp;Ucy;"/>
        <xsl:output-character character="ф" string="&amp;fcy;"/>
        <xsl:output-character character="Ф" string="&amp;Fcy;"/>
        <xsl:output-character character="х" string="&amp;khcy;"/>
        <xsl:output-character character="Х" string="&amp;KHcy;"/>
        <xsl:output-character character="ц" string="&amp;tscy;"/>
        <xsl:output-character character="Ц" string="&amp;TScy;"/>
        <xsl:output-character character="ч" string="&amp;chcy;"/>
        <xsl:output-character character="Ч" string="&amp;CHcy;"/>
        <xsl:output-character character="ш" string="&amp;shcy;"/>
        <xsl:output-character character="Ш" string="&amp;SHcy;"/>
        <xsl:output-character character="щ" string="&amp;shchcy;"/>
        <xsl:output-character character="Щ" string="&amp;SHCHcy;"/>
        <xsl:output-character character="ъ" string="&amp;hardcy;"/>
        <xsl:output-character character="Ъ" string="&amp;HARDcy;"/>
        <xsl:output-character character="ы" string="&amp;ycy;"/>
        <xsl:output-character character="Ы" string="&amp;Ycy;"/>
        <xsl:output-character character="ь" string="&amp;softcy;"/>
        <xsl:output-character character="Ь" string="&amp;SOFTcy;"/>
        <xsl:output-character character="э" string="&amp;ecy;"/>
        <xsl:output-character character="Э" string="&amp;Ecy;"/>
        <xsl:output-character character="ю" string="&amp;yucy;"/>
        <xsl:output-character character="Ю" string="&amp;YUcy;"/>
        <xsl:output-character character="я" string="&amp;yacy;"/>
        <xsl:output-character character="Я" string="&amp;YAcy;"/>
        <xsl:output-character character="№" string="&amp;numero;"/>
        <xsl:output-character character="α" string="&amp;alpha;"/>
        <xsl:output-character character="β" string="&amp;beta;"/>
        <xsl:output-character character="γ" string="&amp;gamma;"/>
        <xsl:output-character character="Γ" string="&amp;Gamma;"/>
        <xsl:output-character character="ϝ" string="&amp;gammad;"/>
        <xsl:output-character character="δ" string="&amp;delta;"/>
        <xsl:output-character character="Δ" string="&amp;Delta;"/>
        <xsl:output-character character="ε" string="&amp;epsi;"/>
        <xsl:output-character character="ϵ" string="&amp;epsiv;"/>
        <xsl:output-character character="ζ" string="&amp;zeta;"/>
        <xsl:output-character character="η" string="&amp;eta;"/>
        <xsl:output-character character="Θ" string="&amp;Theta;"/>
        <xsl:output-character character="ϑ" string="&amp;thetav;"/>
        <xsl:output-character character="ι" string="&amp;iota;"/>
        <xsl:output-character character="κ" string="&amp;kappa;"/>
        <xsl:output-character character="ϰ" string="&amp;kappav;"/>
        <xsl:output-character character="λ" string="&amp;lambda;"/>
        <xsl:output-character character="Λ" string="&amp;Lambda;"/>
        <xsl:output-character character="μ" string="&amp;mu;"/>
        <xsl:output-character character="ν" string="&amp;nu;"/>
        <xsl:output-character character="ξ" string="&amp;xi;"/>
        <xsl:output-character character="Ξ" string="&amp;Xi;"/>
        <xsl:output-character character="π" string="&amp;pi;"/>
        <xsl:output-character character="ϖ" string="&amp;piv;"/>
        <xsl:output-character character="Π" string="&amp;Pi;"/>
        <xsl:output-character character="ρ" string="&amp;rho;"/>
        <xsl:output-character character="ϱ" string="&amp;rhov;"/>
        <xsl:output-character character="σ" string="&amp;sigma;"/>
        <xsl:output-character character="Σ" string="&amp;Sigma;"/>
        <xsl:output-character character="ς" string="&amp;sigmav;"/>
        <xsl:output-character character="τ" string="&amp;tau;"/>
        <xsl:output-character character="υ" string="&amp;upsi;"/>
        <xsl:output-character character="ϒ" string="&amp;Upsi;"/>
        <xsl:output-character character="Φ" string="&amp;Phi;"/>
        <xsl:output-character character="ϕ" string="&amp;phiv;"/>
        <xsl:output-character character="χ" string="&amp;chi;"/>
        <xsl:output-character character="ψ" string="&amp;psi;"/>
        <xsl:output-character character="Ψ" string="&amp;Psi;"/>
        <xsl:output-character character="ω" string="&amp;omega;"/>
        <xsl:output-character character="Ω" string="&amp;Omega;"/>
        <xsl:output-character character="á" string="&amp;aacute;"/>
        <xsl:output-character character="Á" string="&amp;Aacute;"/>
        <xsl:output-character character="â" string="&amp;acirc;"/>
        <xsl:output-character character="Â" string="&amp;Acirc;"/>
        <xsl:output-character character="à" string="&amp;agrave;"/>
        <xsl:output-character character="À" string="&amp;Agrave;"/>
        <xsl:output-character character="å" string="&amp;aring;"/>
        <xsl:output-character character="Å" string="&amp;Aring;"/>
        <xsl:output-character character="ã" string="&amp;atilde;"/>
        <xsl:output-character character="Ã" string="&amp;Atilde;"/>
        <xsl:output-character character="ä" string="&amp;auml;"/>
        <xsl:output-character character="Ä" string="&amp;Auml;"/>
        <xsl:output-character character="æ" string="&amp;aelig;"/>
        <xsl:output-character character="Æ" string="&amp;AElig;"/>
        <xsl:output-character character="ç" string="&amp;ccedil;"/>
        <xsl:output-character character="Ç" string="&amp;Ccedil;"/>
        <xsl:output-character character="ð" string="&amp;eth;"/>
        <xsl:output-character character="Ð" string="&amp;ETH;"/>
        <xsl:output-character character="é" string="&amp;eacute;"/>
        <xsl:output-character character="É" string="&amp;Eacute;"/>
        <xsl:output-character character="ê" string="&amp;ecirc;"/>
        <xsl:output-character character="Ê" string="&amp;Ecirc;"/>
        <xsl:output-character character="è" string="&amp;egrave;"/>
        <xsl:output-character character="È" string="&amp;Egrave;"/>
        <xsl:output-character character="ë" string="&amp;euml;"/>
        <xsl:output-character character="Ë" string="&amp;Euml;"/>
        <xsl:output-character character="í" string="&amp;iacute;"/>
        <xsl:output-character character="Í" string="&amp;Iacute;"/>
        <xsl:output-character character="î" string="&amp;icirc;"/>
        <xsl:output-character character="Î" string="&amp;Icirc;"/>
        <xsl:output-character character="ì" string="&amp;igrave;"/>
        <xsl:output-character character="Ì" string="&amp;Igrave;"/>
        <xsl:output-character character="ï" string="&amp;iuml;"/>
        <xsl:output-character character="Ï" string="&amp;Iuml;"/>
        <xsl:output-character character="ñ" string="&amp;ntilde;"/>
        <xsl:output-character character="Ñ" string="&amp;Ntilde;"/>
        <xsl:output-character character="ó" string="&amp;oacute;"/>
        <xsl:output-character character="Ó" string="&amp;Oacute;"/>
        <xsl:output-character character="ô" string="&amp;ocirc;"/>
        <xsl:output-character character="Ô" string="&amp;Ocirc;"/>
        <xsl:output-character character="ò" string="&amp;ograve;"/>
        <xsl:output-character character="Ò" string="&amp;Ograve;"/>
        <xsl:output-character character="ø" string="&amp;oslash;"/>
        <xsl:output-character character="Ø" string="&amp;Oslash;"/>
        <xsl:output-character character="õ" string="&amp;otilde;"/>
        <xsl:output-character character="Õ" string="&amp;Otilde;"/>
        <xsl:output-character character="ö" string="&amp;ouml;"/>
        <xsl:output-character character="Ö" string="&amp;Ouml;"/>
        <xsl:output-character character="ß" string="&amp;szlig;"/>
        <xsl:output-character character="þ" string="&amp;thorn;"/>
        <xsl:output-character character="Þ" string="&amp;THORN;"/>
        <xsl:output-character character="ú" string="&amp;uacute;"/>
        <xsl:output-character character="Ú" string="&amp;Uacute;"/>
        <xsl:output-character character="û" string="&amp;ucirc;"/>
        <xsl:output-character character="Û" string="&amp;Ucirc;"/>
        <xsl:output-character character="ù" string="&amp;ugrave;"/>
        <xsl:output-character character="Ù" string="&amp;Ugrave;"/>
        <xsl:output-character character="ü" string="&amp;uuml;"/>
        <xsl:output-character character="Ü" string="&amp;Uuml;"/>
        <xsl:output-character character="ý" string="&amp;yacute;"/>
        <xsl:output-character character="Ý" string="&amp;Yacute;"/>
        <xsl:output-character character="ÿ" string="&amp;yuml;"/>
        <xsl:output-character character="ă" string="&amp;abreve;"/>
        <xsl:output-character character="Ă" string="&amp;Abreve;"/>
        <xsl:output-character character="ā" string="&amp;amacr;"/>
        <xsl:output-character character="Ā" string="&amp;Amacr;"/>
        <xsl:output-character character="ą" string="&amp;aogon;"/>
        <xsl:output-character character="Ą" string="&amp;Aogon;"/>
        <xsl:output-character character="ć" string="&amp;cacute;"/>
        <xsl:output-character character="Ć" string="&amp;Cacute;"/>
        <xsl:output-character character="č" string="&amp;ccaron;"/>
        <xsl:output-character character="Č" string="&amp;Ccaron;"/>
        <xsl:output-character character="ĉ" string="&amp;ccirc;"/>
        <xsl:output-character character="Ĉ" string="&amp;Ccirc;"/>
        <xsl:output-character character="ċ" string="&amp;cdot;"/>
        <xsl:output-character character="Ċ" string="&amp;Cdot;"/>
        <xsl:output-character character="ď" string="&amp;dcaron;"/>
        <xsl:output-character character="Ď" string="&amp;Dcaron;"/>
        <xsl:output-character character="đ" string="&amp;dstrok;"/>
        <xsl:output-character character="Đ" string="&amp;Dstrok;"/>
        <xsl:output-character character="ě" string="&amp;ecaron;"/>
        <xsl:output-character character="Ě" string="&amp;Ecaron;"/>
        <xsl:output-character character="ė" string="&amp;edot;"/>
        <xsl:output-character character="Ė" string="&amp;Edot;"/>
        <xsl:output-character character="ē" string="&amp;emacr;"/>
        <xsl:output-character character="Ē" string="&amp;Emacr;"/>
        <xsl:output-character character="ę" string="&amp;eogon;"/>
        <xsl:output-character character="Ę" string="&amp;Eogon;"/>
        <xsl:output-character character="ǵ" string="&amp;gacute;"/>
        <xsl:output-character character="ğ" string="&amp;gbreve;"/>
        <xsl:output-character character="Ğ" string="&amp;Gbreve;"/>
        <xsl:output-character character="Ģ" string="&amp;Gcedil;"/>
        <xsl:output-character character="ĝ" string="&amp;gcirc;"/>
        <xsl:output-character character="Ĝ" string="&amp;Gcirc;"/>
        <xsl:output-character character="ġ" string="&amp;gdot;"/>
        <xsl:output-character character="Ġ" string="&amp;Gdot;"/>
        <xsl:output-character character="ĥ" string="&amp;hcirc;"/>
        <xsl:output-character character="Ĥ" string="&amp;Hcirc;"/>
        <xsl:output-character character="ħ" string="&amp;hstrok;"/>
        <xsl:output-character character="Ħ" string="&amp;Hstrok;"/>
        <xsl:output-character character="İ" string="&amp;Idot;"/>
        <xsl:output-character character="Ī" string="&amp;Imacr;"/>
        <xsl:output-character character="ī" string="&amp;imacr;"/>
        <xsl:output-character character="ĳ" string="&amp;ijlig;"/>
        <xsl:output-character character="Ĳ" string="&amp;IJlig;"/>
        <xsl:output-character character="ı" string="&amp;inodot;"/>
        <xsl:output-character character="į" string="&amp;iogon;"/>
        <xsl:output-character character="Į" string="&amp;Iogon;"/>
        <xsl:output-character character="ĩ" string="&amp;itilde;"/>
        <xsl:output-character character="Ĩ" string="&amp;Itilde;"/>
        <xsl:output-character character="ĵ" string="&amp;jcirc;"/>
        <xsl:output-character character="Ĵ" string="&amp;Jcirc;"/>
        <xsl:output-character character="ķ" string="&amp;kcedil;"/>
        <xsl:output-character character="Ķ" string="&amp;Kcedil;"/>
        <xsl:output-character character="ĸ" string="&amp;kgreen;"/>
        <xsl:output-character character="ĺ" string="&amp;lacute;"/>
        <xsl:output-character character="Ĺ" string="&amp;Lacute;"/>
        <xsl:output-character character="ľ" string="&amp;lcaron;"/>
        <xsl:output-character character="Ľ" string="&amp;Lcaron;"/>
        <xsl:output-character character="ļ" string="&amp;lcedil;"/>
        <xsl:output-character character="Ļ" string="&amp;Lcedil;"/>
        <xsl:output-character character="ŀ" string="&amp;lmidot;"/>
        <xsl:output-character character="Ŀ" string="&amp;Lmidot;"/>
        <xsl:output-character character="ł" string="&amp;lstrok;"/>
        <xsl:output-character character="Ł" string="&amp;Lstrok;"/>
        <xsl:output-character character="ń" string="&amp;nacute;"/>
        <xsl:output-character character="Ń" string="&amp;Nacute;"/>
        <xsl:output-character character="ŋ" string="&amp;eng;"/>
        <xsl:output-character character="Ŋ" string="&amp;ENG;"/>
        <xsl:output-character character="ŉ" string="&amp;napos;"/>
        <xsl:output-character character="ň" string="&amp;ncaron;"/>
        <xsl:output-character character="Ň" string="&amp;Ncaron;"/>
        <xsl:output-character character="ņ" string="&amp;ncedil;"/>
        <xsl:output-character character="Ņ" string="&amp;Ncedil;"/>
        <xsl:output-character character="ő" string="&amp;odblac;"/>
        <xsl:output-character character="Ő" string="&amp;Odblac;"/>
        <xsl:output-character character="Ō" string="&amp;Omacr;"/>
        <xsl:output-character character="ō" string="&amp;omacr;"/>
        <xsl:output-character character="œ" string="&amp;oelig;"/>
        <xsl:output-character character="Œ" string="&amp;OElig;"/>
        <xsl:output-character character="ŕ" string="&amp;racute;"/>
        <xsl:output-character character="Ŕ" string="&amp;Racute;"/>
        <xsl:output-character character="ř" string="&amp;rcaron;"/>
        <xsl:output-character character="Ř" string="&amp;Rcaron;"/>
        <xsl:output-character character="ŗ" string="&amp;rcedil;"/>
        <xsl:output-character character="Ŗ" string="&amp;Rcedil;"/>
        <xsl:output-character character="ś" string="&amp;sacute;"/>
        <xsl:output-character character="Ś" string="&amp;Sacute;"/>
        <xsl:output-character character="š" string="&amp;scaron;"/>
        <xsl:output-character character="Š" string="&amp;Scaron;"/>
        <xsl:output-character character="ş" string="&amp;scedil;"/>
        <xsl:output-character character="Ş" string="&amp;Scedil;"/>
        <xsl:output-character character="ŝ" string="&amp;scirc;"/>
        <xsl:output-character character="Ŝ" string="&amp;Scirc;"/>
        <xsl:output-character character="ť" string="&amp;tcaron;"/>
        <xsl:output-character character="Ť" string="&amp;Tcaron;"/>
        <xsl:output-character character="ţ" string="&amp;tcedil;"/>
        <xsl:output-character character="Ţ" string="&amp;Tcedil;"/>
        <xsl:output-character character="ŧ" string="&amp;tstrok;"/>
        <xsl:output-character character="Ŧ" string="&amp;Tstrok;"/>
        <xsl:output-character character="ŭ" string="&amp;ubreve;"/>
        <xsl:output-character character="Ŭ" string="&amp;Ubreve;"/>
        <xsl:output-character character="ű" string="&amp;udblac;"/>
        <xsl:output-character character="Ű" string="&amp;Udblac;"/>
        <xsl:output-character character="ū" string="&amp;umacr;"/>
        <xsl:output-character character="Ū" string="&amp;Umacr;"/>
        <xsl:output-character character="ų" string="&amp;uogon;"/>
        <xsl:output-character character="Ų" string="&amp;Uogon;"/>
        <xsl:output-character character="ů" string="&amp;uring;"/>
        <xsl:output-character character="Ů" string="&amp;Uring;"/>
        <xsl:output-character character="ũ" string="&amp;utilde;"/>
        <xsl:output-character character="Ũ" string="&amp;Utilde;"/>
        <xsl:output-character character="ŵ" string="&amp;wcirc;"/>
        <xsl:output-character character="Ŵ" string="&amp;Wcirc;"/>
        <xsl:output-character character="ŷ" string="&amp;ycirc;"/>
        <xsl:output-character character="Ŷ" string="&amp;Ycirc;"/>
        <xsl:output-character character="Ÿ" string="&amp;Yuml;"/>
        <xsl:output-character character="ź" string="&amp;zacute;"/>
        <xsl:output-character character="Ź" string="&amp;Zacute;"/>
        <xsl:output-character character="ž" string="&amp;zcaron;"/>
        <xsl:output-character character="Ž" string="&amp;Zcaron;"/>
        <xsl:output-character character="ż" string="&amp;zdot;"/>
        <xsl:output-character character="Ż" string="&amp;Zdot;"/>
        <xsl:output-character character="½" string="&amp;half;"/>
        <xsl:output-character character="½" string="&amp;frac12;"/>
        <xsl:output-character character="¼" string="&amp;frac14;"/>
        <xsl:output-character character="¾" string="&amp;frac34;"/>
        <xsl:output-character character="⅛" string="&amp;frac18;"/>
        <xsl:output-character character="⅜" string="&amp;frac38;"/>
        <xsl:output-character character="⅝" string="&amp;frac58;"/>
        <xsl:output-character character="⅞" string="&amp;frac78;"/>
        <xsl:output-character character="¹" string="&amp;sup1;"/>
        <xsl:output-character character="²" string="&amp;sup2;"/>
        <xsl:output-character character="³" string="&amp;sup3;"/>
        <xsl:output-character character="±" string="&amp;plusmn;"/>
        <xsl:output-character character="÷" string="&amp;divide;"/>
        <xsl:output-character character="×" string="&amp;times;"/>
        <xsl:output-character character="¤" string="&amp;curren;"/>
        <xsl:output-character character="£" string="&amp;pound;"/>
        <xsl:output-character character="$" string="&amp;dollar;"/>
        <xsl:output-character character="¢" string="&amp;cent;"/>
        <xsl:output-character character="¥" string="&amp;yen;"/>
        <xsl:output-character character="―" string="&amp;horbar;"/>
        <xsl:output-character character="µ" string="&amp;micro;"/>
        <xsl:output-character character="Ω" string="&amp;ohm;"/>
        <xsl:output-character character="°" string="&amp;deg;"/>
        <xsl:output-character character="º" string="&amp;ordm;"/>
        <xsl:output-character character="ª" string="&amp;ordf;"/>
        <xsl:output-character character="§" string="&amp;sect;"/>
        <xsl:output-character character="¶" string="&amp;para;"/>
        <xsl:output-character character="·" string="&amp;middot;"/>
        <xsl:output-character character="←" string="&amp;larr;"/>
        <xsl:output-character character="→" string="&amp;rarr;"/>
        <xsl:output-character character="↑" string="&amp;uarr;"/>
        <xsl:output-character character="↓" string="&amp;darr;"/>
        <xsl:output-character character="©" string="&amp;copy;"/>
        <xsl:output-character character="®" string="&amp;reg;"/>
        <xsl:output-character character="™" string="&amp;trade;"/>
        <xsl:output-character character="¦" string="&amp;brvbar;"/>
        <xsl:output-character character="¬" string="&amp;not;"/>
        <xsl:output-character character="♪" string="&amp;sung;"/>
        <xsl:output-character character="¡" string="&amp;iexcl;"/>
        <xsl:output-character character="‐" string="&amp;hyphen;"/>
        <xsl:output-character character="¿" string="&amp;iquest;"/>
        <xsl:output-character character="«" string="&amp;laquo;"/>
        <xsl:output-character character="»" string="&amp;raquo;"/>
        <xsl:output-character character="‘" string="&amp;lsquo;"/>
        <xsl:output-character character="’" string="&amp;rsquo;"/>
        <xsl:output-character character="“" string="&amp;ldquo;"/>
        <xsl:output-character character="”" string="&amp;rdquo;"/>
        <xsl:output-character character="­" string="&amp;shy;"/>
        <xsl:output-character character=" " string="&amp;emsp;"/>
        <xsl:output-character character=" " string="&amp;ensp;"/>
        <xsl:output-character character=" " string="&amp;emsp13;"/>
        <xsl:output-character character=" " string="&amp;emsp14;"/>
        <xsl:output-character character=" " string="&amp;numsp;"/>
        <xsl:output-character character=" " string="&amp;puncsp;"/>
        <xsl:output-character character=" " string="&amp;thinsp;"/>
        <xsl:output-character character=" " string="&amp;hairsp;"/>
        <xsl:output-character character="—" string="&amp;mdash;"/>
        <xsl:output-character character="–" string="&amp;ndash;"/>
        <xsl:output-character character="‐" string="&amp;dash;"/>
        <xsl:output-character character="␣" string="&amp;blank;"/>
        <xsl:output-character character="…" string="&amp;hellip;"/>
        <xsl:output-character character="‥" string="&amp;nldr;"/>
        <xsl:output-character character="⅓" string="&amp;frac13;"/>
        <xsl:output-character character="⅔" string="&amp;frac23;"/>
        <xsl:output-character character="⅕" string="&amp;frac15;"/>
        <xsl:output-character character="⅖" string="&amp;frac25;"/>
        <xsl:output-character character="⅗" string="&amp;frac35;"/>
        <xsl:output-character character="⅘" string="&amp;frac45;"/>
        <xsl:output-character character="⅙" string="&amp;frac16;"/>
        <xsl:output-character character="⅚" string="&amp;frac56;"/>
        <xsl:output-character character="℅" string="&amp;incare;"/>
        <xsl:output-character character="█" string="&amp;block;"/>
        <xsl:output-character character="▀" string="&amp;uhblk;"/>
        <xsl:output-character character="▄" string="&amp;lhblk;"/>
        <xsl:output-character character="░" string="&amp;blk14;"/>
        <xsl:output-character character="▒" string="&amp;blk12;"/>
        <xsl:output-character character="▓" string="&amp;blk34;"/>
        <xsl:output-character character="▮" string="&amp;marker;"/>
        <xsl:output-character character="○" string="&amp;cir;"/>
        <xsl:output-character character="□" string="&amp;squ;"/>
        <xsl:output-character character="▭" string="&amp;rect;"/>
        <xsl:output-character character="▵" string="&amp;utri;"/>
        <xsl:output-character character="▿" string="&amp;dtri;"/>
        <xsl:output-character character="☆" string="&amp;star;"/>
        <xsl:output-character character="•" string="&amp;bull;"/>
        <xsl:output-character character="▪" string="&amp;squf;"/>
        <xsl:output-character character="▴" string="&amp;utrif;"/>
        <xsl:output-character character="▾" string="&amp;dtrif;"/>
        <xsl:output-character character="◂" string="&amp;ltrif;"/>
        <xsl:output-character character="▸" string="&amp;rtrif;"/>
        <xsl:output-character character="♣" string="&amp;clubs;"/>
        <xsl:output-character character="♦" string="&amp;diams;"/>
        <xsl:output-character character="♥" string="&amp;hearts;"/>
        <xsl:output-character character="♠" string="&amp;spades;"/>
        <xsl:output-character character="✠" string="&amp;malt;"/>
        <xsl:output-character character="†" string="&amp;dagger;"/>
        <xsl:output-character character="‡" string="&amp;Dagger;"/>
        <xsl:output-character character="✓" string="&amp;check;"/>
        <xsl:output-character character="✗" string="&amp;cross;"/>
        <xsl:output-character character="♯" string="&amp;sharp;"/>
        <xsl:output-character character="♭" string="&amp;flat;"/>
        <xsl:output-character character="♂" string="&amp;male;"/>
        <xsl:output-character character="♀" string="&amp;female;"/>
        <xsl:output-character character="☎" string="&amp;phone;"/>
        <xsl:output-character character="⌕" string="&amp;telrec;"/>
        <xsl:output-character character="℗" string="&amp;copysr;"/>
        <xsl:output-character character="⁁" string="&amp;caret;"/>
        <xsl:output-character character="‚" string="&amp;lsquor;"/>
        <xsl:output-character character="„" string="&amp;ldquor;"/>
        <xsl:output-character character="ﬀ" string="&amp;fflig;"/>
        <xsl:output-character character="ﬁ" string="&amp;filig;"/>
        <xsl:output-character character="ﬃ" string="&amp;ffilig;"/>
        <xsl:output-character character="ﬄ" string="&amp;ffllig;"/>
        <xsl:output-character character="ﬂ" string="&amp;fllig;"/>
        <xsl:output-character character="…" string="&amp;mldr;"/>
        <xsl:output-character character="”" string="&amp;rdquor;"/>
        <xsl:output-character character="’" string="&amp;rsquor;"/>
        <xsl:output-character character="⋮" string="&amp;vellip;"/>
        <xsl:output-character character="⁃" string="&amp;hybull;"/>
        <xsl:output-character character="◊" string="&amp;loz;"/>
        <xsl:output-character character="⧫" string="&amp;lozf;"/>
        <xsl:output-character character="◃" string="&amp;ltri;"/>
        <xsl:output-character character="▹" string="&amp;rtri;"/>
        <xsl:output-character character="★" string="&amp;starf;"/>
        <xsl:output-character character="♮" string="&amp;natur;"/>
        <xsl:output-character character="℞" string="&amp;rx;"/>
        <xsl:output-character character="✶" string="&amp;sext;"/>
        <xsl:output-character character="⌖" string="&amp;target;"/>
        <xsl:output-character character="⌍" string="&amp;dlcrop;"/>
        <xsl:output-character character="⌌" string="&amp;drcrop;"/>
        <xsl:output-character character="⌏" string="&amp;ulcrop;"/>
        <xsl:output-character character="⌎" string="&amp;urcrop;"/>
        <xsl:output-character character="ℵ" string="&amp;aleph;"/>
        <xsl:output-character character="∧" string="&amp;and;"/>
        <xsl:output-character character="∢" string="&amp;angsph;"/>
        <xsl:output-character character="≈" string="&amp;ap;"/>
        <xsl:output-character character="∵" string="&amp;becaus;"/>
        <xsl:output-character character="⊥" string="&amp;bottom;"/>
        <xsl:output-character character="∩" string="&amp;cap;"/>
        <xsl:output-character character="≅" string="&amp;cong;"/>
        <xsl:output-character character="∮" string="&amp;conint;"/>
        <xsl:output-character character="∪" string="&amp;cup;"/>
        <xsl:output-character character="≡" string="&amp;equiv;"/>
        <xsl:output-character character="∃" string="&amp;exist;"/>
        <xsl:output-character character="∀" string="&amp;forall;"/>
        <xsl:output-character character="ƒ" string="&amp;fnof;"/>
        <xsl:output-character character="≥" string="&amp;ge;"/>
        <xsl:output-character character="⇔" string="&amp;iff;"/>
        <xsl:output-character character="∞" string="&amp;infin;"/>
        <xsl:output-character character="∫" string="&amp;int;"/>
        <xsl:output-character character="∈" string="&amp;isin;"/>
        <xsl:output-character character="⟨" string="&amp;lang;"/>
        <xsl:output-character character="⇐" string="&amp;lArr;"/>
        <xsl:output-character character="≤" string="&amp;le;"/>
        <xsl:output-character character="−" string="&amp;minus;"/>
        <xsl:output-character character="∓" string="&amp;mnplus;"/>
        <xsl:output-character character="∇" string="&amp;nabla;"/>
        <xsl:output-character character="≠" string="&amp;ne;"/>
        <xsl:output-character character="∋" string="&amp;ni;"/>
        <xsl:output-character character="∨" string="&amp;or;"/>
        <xsl:output-character character="∥" string="&amp;par;"/>
        <xsl:output-character character="∂" string="&amp;part;"/>
        <xsl:output-character character="‰" string="&amp;permil;"/>
        <xsl:output-character character="⊥" string="&amp;perp;"/>
        <xsl:output-character character="′" string="&amp;prime;"/>
        <xsl:output-character character="″" string="&amp;Prime;"/>
        <xsl:output-character character="∝" string="&amp;prop;"/>
        <xsl:output-character character="√" string="&amp;radic;"/>
        <xsl:output-character character="⟩" string="&amp;rang;"/>
        <xsl:output-character character="⇒" string="&amp;rArr;"/>
        <xsl:output-character character="∼" string="&amp;sim;"/>
        <xsl:output-character character="≃" string="&amp;sime;"/>
        <xsl:output-character character="□" string="&amp;square;"/>
        <xsl:output-character character="⊂" string="&amp;sub;"/>
        <xsl:output-character character="⊆" string="&amp;sube;"/>
        <xsl:output-character character="⊃" string="&amp;sup;"/>
        <xsl:output-character character="⊇" string="&amp;supe;"/>
        <xsl:output-character character="∴" string="&amp;there4;"/>
        <xsl:output-character character="‖" string="&amp;Verbar;"/>
        <xsl:output-character character="Å" string="&amp;angst;"/>
        <xsl:output-character character="ℬ" string="&amp;bernou;"/>
        <xsl:output-character character="∘" string="&amp;compfn;"/>
        <xsl:output-character character="¨" string="&amp;Dot;"/>
        <xsl:output-character character="⃜" string="&amp;DotDot;"/>
        <xsl:output-character character="ℋ" string="&amp;hamilt;"/>
        <xsl:output-character character="ℒ" string="&amp;lagran;"/>
        <xsl:output-character character="∗" string="&amp;lowast;"/>
        <xsl:output-character character="∉" string="&amp;notin;"/>
        <xsl:output-character character="ℴ" string="&amp;order;"/>
        <xsl:output-character character="ℳ" string="&amp;phmmat;"/>
        <xsl:output-character character="⃛" string="&amp;tdot;"/>
        <xsl:output-character character="‴" string="&amp;tprime;"/>
        <xsl:output-character character="≙" string="&amp;wedgeq;"/>
        <xsl:output-character character=" " string="&amp;nbsp;"/>
    </xsl:character-map>-->
    <!--<xsl:variable name="fname" select="tokenize(base-uri(), '/')[last()]"/>
    <xsl:variable name="fnamewithoutext" select="replace(replace($fname,'_final',''), '.xml','')"/>-->
    <xsl:output method="xml" omit-xml-declaration="no"/>
    
    <xsl:template match="@*|node()">
        <xsl:copy>
            <xsl:apply-templates select="@*|node()"/>
        </xsl:copy>
    </xsl:template>
    
    <xsl:template match="//al[not(parent::theme)]">
        <xsl:choose>
            <xsl:when test="preceding-sibling::*[1][self::motscles] and following-sibling::*[1][self::al]">
                <xsl:text disable-output-escaping="yes"><![CDATA[<notes>]]></xsl:text>
                <xsl:copy>
                    <xsl:copy-of select="@*"/>
                    <xsl:apply-templates/>
                </xsl:copy>
            </xsl:when>
            <xsl:when test="preceding-sibling::*[1][self::motscles] and not(following-sibling::*[1][self::al])">
                <xsl:text disable-output-escaping="yes"><![CDATA[<notes>]]></xsl:text>
                <xsl:copy>
                    <xsl:copy-of select="@*"/>
                    <xsl:apply-templates/>
                </xsl:copy>
                <xsl:text disable-output-escaping="yes"><![CDATA[</notes>]]></xsl:text>
            </xsl:when>
            <xsl:when test="preceding-sibling::motscles and not(following-sibling::*[1][self::al]) and preceding-sibling::*[1][self::al]">
                <xsl:copy>
                    <xsl:copy-of select="@*"/>
                    <xsl:apply-templates/>
                </xsl:copy>
                <xsl:text disable-output-escaping="yes"><![CDATA[</notes>]]></xsl:text>
            </xsl:when>
            <xsl:when test="parent::div1chr or parent::div2chr">
                <xsl:choose>
                    <xsl:when test="not(preceding-sibling::*[1][self::al]) and following-sibling::*[1][self::al] and not(preceding-sibling::*[1][self::nb-bl]) and not(preceding-sibling::*[1][self::cita-bl])  and not(following-sibling::*[1][self::div2chr])">
                        <xsl:text disable-output-escaping="yes"><![CDATA[<pnchr><observ>]]></xsl:text>
                        <xsl:copy>
                            <xsl:copy-of select="@*"/>
                            <xsl:apply-templates/>
                        </xsl:copy>
                    </xsl:when>
                    <xsl:when test="not(following-sibling::*[1][self::al]) and preceding-sibling::*[1][self::al] and not(following-sibling::*[1][self::nb-bl]) and not(following-sibling::*[1][self::cita-bl]) and not(following-sibling::*[1][self::div2chr])">
                        <xsl:copy>
                            <xsl:copy-of select="@*"/>
                            <xsl:apply-templates/>
                        </xsl:copy>
                        <xsl:text disable-output-escaping="yes"><![CDATA[</observ></pnchr>]]></xsl:text>
                    </xsl:when>
                    <xsl:when test="not(following-sibling::*[1][self::al]) and not(preceding-sibling::*[1][self::al])">
                        <xsl:text disable-output-escaping="yes"><![CDATA[<pnchr><observ>]]></xsl:text>
                        <xsl:copy>
                            <xsl:copy-of select="@*"/>
                            <xsl:apply-templates/>
                        </xsl:copy>
                        <xsl:text disable-output-escaping="yes"><![CDATA[</observ></pnchr>]]></xsl:text>
                    </xsl:when>
                    <xsl:otherwise>
                        <xsl:copy>
                            <xsl:copy-of select="@*"/>
                            <xsl:apply-templates/>
                        </xsl:copy>
                    </xsl:otherwise>
                    
                </xsl:choose>
            </xsl:when>
            <xsl:when test="parent::div1txt or parent::div2txt or parent::div3txt or parent::div4txt or parent::div5txt or parent::div6txt or parent::div7txt or parent::div8txt or parent::div9txt or parent::div10txt">
                <xsl:choose>
                    <xsl:when test="not(preceding-sibling::*[1][self::al]) and following-sibling::*[1][self::al] and not(preceding-sibling::*[1][self::nb-bl]) and not(preceding-sibling::*[1][self::cita-bl])  and not(following-sibling::*[1][self::div2chr])">
                        <xsl:text disable-output-escaping="yes"><![CDATA[<p>]]></xsl:text>
                        <xsl:copy>
                            <xsl:copy-of select="@*"/>
                            <xsl:apply-templates/>
                        </xsl:copy>
                    </xsl:when>
                    <xsl:when test="not(following-sibling::*[1][self::al]) and preceding-sibling::*[1][self::al] and not(following-sibling::*[1][self::nb-bl]) and not(following-sibling::*[1][self::cita-bl]) and not(following-sibling::*[1][self::div2chr])">
                        <xsl:copy>
                            <xsl:copy-of select="@*"/>
                            <xsl:apply-templates/>
                        </xsl:copy>
                        <xsl:text disable-output-escaping="yes"><![CDATA[</p>]]></xsl:text>
                    </xsl:when>
                    <xsl:when test="not(following-sibling::*[1][self::al]) and not(preceding-sibling::*[1][self::al])">
                        <xsl:text disable-output-escaping="yes"><![CDATA[<p>]]></xsl:text>
                        <xsl:copy>
                            <xsl:copy-of select="@*"/>
                            <xsl:apply-templates/>
                        </xsl:copy>
                        <xsl:text disable-output-escaping="yes"><![CDATA[</p>]]></xsl:text>
                    </xsl:when>
                    <xsl:otherwise>
                        <xsl:copy>
                            <xsl:copy-of select="@*"/>
                            <xsl:apply-templates/>
                        </xsl:copy>
                    </xsl:otherwise>
                    
                </xsl:choose>
            </xsl:when>
            <xsl:otherwise>
                <xsl:copy>
                    <xsl:copy-of select="@*"/>
                    <xsl:apply-templates/>
                </xsl:copy>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>
    
    <!--<xsl:template match="//al">
        <xsl:choose>
            <xsl:when test="parent::annexe">
                <p>
                    <xsl:copy>
                        <xsl:copy-of select="@*"/>
                        <xsl:choose>
                            <xsl:when test="matches(.,'\[coche\]')">
                                <xsl:apply-templates select="replace(.,'\[coche\]','')"/>
                            </xsl:when>
                            <xsl:otherwise>
                                <xsl:apply-templates select="node()"/>
                            </xsl:otherwise>
                        </xsl:choose>
                    </xsl:copy> 
                </p>
            </xsl:when>
            <xsl:otherwise>
                <xsl:copy>
                    <xsl:copy-of select="@*"/>
                    <xsl:choose>
                        <xsl:when test="matches(.,'\[coche\]')">
                            <xsl:apply-templates select="replace(.,'\[coche\]','')"/>
                        </xsl:when>
                        <xsl:otherwise>
                            <xsl:apply-templates select="node()"/>
                        </xsl:otherwise>
                    </xsl:choose>
                </xsl:copy>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>-->
    
    <xsl:template match="//tit/al/emph3">
        <emph2>
            <xsl:apply-templates select="node()|@*"/>
        </emph2>
    </xsl:template>
    <xsl:template match="//nom">
        <nom>
            <xsl:variable name="nomtxt"><xsl:value-of select="."/></xsl:variable>
            <xsl:for-each select="tokenize($nomtxt, ' ')">
                <xsl:choose>
                    <xsl:when test="position()!=last()">
                        <xsl:call-template name="CamelCase">
                            <xsl:with-param name="text"><xsl:value-of select="."/></xsl:with-param>
                        </xsl:call-template>
                        <xsl:text> </xsl:text>
                    </xsl:when>
                    <xsl:otherwise>
                        <xsl:call-template name="CamelCase">
                            <xsl:with-param name="text"><xsl:value-of select="."/></xsl:with-param>
                        </xsl:call-template>
                    </xsl:otherwise>                    
                </xsl:choose>
            </xsl:for-each>
            <!--<xsl:value-of select="lower-case(.)"/>-->
        </nom>
    </xsl:template>
    
    <xsl:template name="CamelCase">
        <xsl:param name="text"/>
        <xsl:choose>
            <xsl:when test="contains($text,'-')">
                <xsl:call-template name="CamelCaseWord">
                    <xsl:with-param name="text" select="substring-before($text,'-')"/>
                </xsl:call-template>
                <xsl:text>-</xsl:text>
                <xsl:call-template name="CamelCaseWord">
                    <xsl:with-param name="text" select="substring-after($text,'-')"/>
                </xsl:call-template>
            </xsl:when>
            <xsl:when test="contains($text,'’')">
                <xsl:call-template name="CamelCaseWord">
                    <xsl:with-param name="text" select="substring-before($text,'’')"/>
                </xsl:call-template>
                <xsl:text>’</xsl:text>
                <xsl:call-template name="CamelCaseWord">
                    <xsl:with-param name="text" select="substring-after($text,'’')"/>
                </xsl:call-template>
            </xsl:when>
            <xsl:when test="contains($text,'''')">
                <xsl:call-template name="CamelCaseWord">
                    <xsl:with-param name="text" select="substring-before($text,'''')"/>
                </xsl:call-template>
                <xsl:text>'</xsl:text>
                <xsl:call-template name="CamelCaseWord">
                    <xsl:with-param name="text" select="substring-after($text,'''')"/>
                </xsl:call-template>
            </xsl:when>
            <xsl:when test="contains($text,' ')">
                <xsl:call-template name="CamelCaseWord">
                    <xsl:with-param name="text" select="substring-before($text,' ')"/>
                </xsl:call-template>
                <xsl:text> </xsl:text>
                <xsl:call-template name="CamelCaseWord">
                    <xsl:with-param name="text" select="substring-after($text,' ')"/>
                </xsl:call-template>
            </xsl:when>
            <xsl:otherwise>
                <xsl:call-template name="CamelCaseWord">
                    <xsl:with-param name="text" select="$text"/>
                </xsl:call-template>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>
    <xsl:template name="CamelCaseWord">
        <xsl:param name="text"/>
        <xsl:value-of select="translate(substring($text,1,1),'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')" /><xsl:value-of select="translate(substring($text,2,string-length($text)-1),'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz')" />
    </xsl:template>
    
    <xsl:template match="//corpsetu[child::al]|//introdossier[child::al]|//resume[child::al]|//corpstxt[child::al]">
        <xsl:copy>
            <xsl:copy-of select="@*"/>
            <xsl:for-each-group select="*" group-adjacent="boolean(self::al)">
                <xsl:choose>
                    <xsl:when test="current-grouping-key()">
                        <xsl:text>&#x0A;</xsl:text>
                        <p>
                            <xsl:text>&#x0A;</xsl:text>
                            <xsl:apply-templates select="current-group()"/>
                            <xsl:text>&#x0A;</xsl:text>
                        </p>
                        <xsl:text>&#x0A;</xsl:text>
                    </xsl:when>
                    <xsl:otherwise>
                        <xsl:apply-templates select="current-group()"/>
                    </xsl:otherwise>
                </xsl:choose>
            </xsl:for-each-group>
        </xsl:copy>
    </xsl:template>
    <xsl:template match="//div1etu[child::al]|//div2etu[child::al]|//div3etu[child::al]">
        <xsl:copy>
            <xsl:copy-of select="@*"/>
            <xsl:for-each-group select="*" group-adjacent="boolean(self::al)">
                <xsl:choose>
                    <xsl:when test="current-grouping-key()">
                        <xsl:text>&#x0A;</xsl:text>
                        <pn>
                            <xsl:text>&#x0A;</xsl:text>
                            <xsl:apply-templates select="current-group()"/>
                            <xsl:text>&#x0A;</xsl:text>
                        </pn>
                        <xsl:text>&#x0A;</xsl:text>
                    </xsl:when>
                    <xsl:otherwise>
                        <xsl:apply-templates select="current-group()"/>
                    </xsl:otherwise>
                </xsl:choose>
            </xsl:for-each-group>
        </xsl:copy>
    </xsl:template>
    <xsl:template match="//div3fiprat|//div3ess|//div3rfn|//div3alerte|//div3etu|//div3apercu|//div3txt|//div3chr|//div3com">
        <xsl:copy>
            <xsl:apply-templates select="node()[name()!='motscles' and name()!='annexes' and name()!='refsource' and name()!='grouprefsource' and name()!= 'sourcex' and name()!='reftxt' and name()!='refsjc' and name()!='refslnf' and name()!='refsdossier' and name()!='refbib' and name()!='divref' and name()!='divbib' and name()!='refjp']"/>
        </xsl:copy>
    </xsl:template>
    <xsl:template match="div2fiprat|div2ess|div2rfn|div2alerte|div2etu|div2apercu|div2txt|//div2chr|//div2com">
        <xsl:copy>
            <xsl:apply-templates select="node()[name()!='auteur' and name()!='motscles' and name()!='annexes' and name()!='refsource' and name()!='grouprefsource' and name()!= 'sourcex' and name()!='reftxt' and name()!='refsjc' and name()!='refslnf' and name()!='refsdossier' and name()!='refbib' and name()!='divref' and name()!='divbib' and name()!='refjp']"/>
        </xsl:copy>
    </xsl:template>
    <xsl:template match="div1fiprat|div1ess|div1rfn|div1alerte|div1etu|div1apercu|div1txt|//div1chr|//div1com">
        <xsl:copy>
            <xsl:apply-templates select="node()[name()!='auteur' and name()!='motscles' and name()!='annexes' and name()!='refsource' and name()!='grouprefsource' and name()!= 'sourcex' and name()!='reftxt' and name()!='refsjc' and name()!='refslnf' and name()!='refsdossier' and name()!='refbib' and name()!='divref' and name()!='divbib' and name()!='refjp']"/>
        </xsl:copy>
    </xsl:template>
    <xsl:template match="div4fiprat|div4ess|div4rfn|div4alerte|div4etu|div4apercu|div4txt|//div4chr|//div4com">
        <xsl:copy>
            <xsl:apply-templates select="node()[name()!='motscles' and name()!='annexes' and name()!='refsource' and name()!='grouprefsource' and name()!= 'sourcex' and name()!='reftxt' and name()!='refsjc' and name()!='refslnf' and name()!='refsdossier' and name()!='refbib' and name()!='divref' and name()!='divbib' and name()!='refjp']"/>
        </xsl:copy>
    </xsl:template>
    
    <xsl:template match="//pnchr">
        <xsl:copy>
        <xsl:choose>
            <xsl:when test="preceding-sibling::*[1][self::grouprefsource]">
                <xsl:apply-templates select="preceding-sibling::*[1][self::grouprefsource]"/>
                <xsl:apply-templates select="node()"/>
            </xsl:when>
            <xsl:otherwise>
                <xsl:apply-templates select="node()"/>
            </xsl:otherwise>
        </xsl:choose>
        </xsl:copy>
    </xsl:template>
    <xsl:template match="//grouprefsource">
        <xsl:apply-templates/>
    </xsl:template>
    <xsl:template match="//refsource">
        <xsl:copy>
            <xsl:copy-of select="@*"/>
            <xsl:value-of select="replace(.,' \[invisible\]','')"/>
        </xsl:copy>
    </xsl:template>
    <xsl:template match="//tinoye">
        <xsl:choose>
            <xsl:when test="string-length()!=0">
                <xsl:choose>
                    <xsl:when test="parent::falerte|parent::observ|parent::pn">
                        <xsl:copy>
                            <xsl:apply-templates select="node()"/>
                        </xsl:copy>
                    </xsl:when>
                    <xsl:otherwise>
                        <al>
                            <xsl:apply-templates select="node()"/>
                        </al>
                    </xsl:otherwise>
                </xsl:choose>
            </xsl:when>
        </xsl:choose>
    </xsl:template>
    <xsl:template match="//tinoye[child::sampletit]"/>
    
    <xsl:template match="fnotes[not(child::fnote)]"/>
    <!--<xsl:template match="//resume">
        <xsl:choose>
            <xsl:when test="ancestor::apercu">
                <xsl:message terminate="yes">Error: resume is not allowed for apercu type</xsl:message>
            </xsl:when>
        </xsl:choose>
    </xsl:template>-->
    <xsl:template match="text()">
        <xsl:value-of select="replace(., '&#10;&#10;+', '&#10;')"/>
    </xsl:template>
    
    <xsl:template match="//imag">
        <xsl:choose>
            <xsl:when test="matches(descendant::apimag/@fic, '[Ii][Nn][Vv][Ii][Ss][Ii][Bb][Ll][Ee]')"></xsl:when>
            <xsl:otherwise>
                <xsl:copy>
                    <xsl:text>&#x0A;</xsl:text>
                    <xsl:if test="preceding-sibling::*[1][self::tiimag]">
                        <xsl:copy-of select="preceding-sibling::*[1][self::tiimag]"/>
                    </xsl:if>
                    <xsl:text>&#x0A;</xsl:text>
                    <xsl:if test="following-sibling::*[1][self::legimag]">
                        <xsl:copy-of select="following-sibling::*[1][self::legimag]"/>
                    </xsl:if>
                    <xsl:apply-templates select="node()|@*"/>
                </xsl:copy>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>
    <xsl:template match="//tiimag[following-sibling::*[1][self::imag]]"/>
    <xsl:template match="//legimag[preceding-sibling::*[1][self::imag]]"/>
    <xsl:template match="//tab">
        <xsl:copy>
            <xsl:if test="preceding-sibling::*[1][self::titab]">
                <xsl:copy-of select="preceding-sibling::*[1][self::titab]"/>
            </xsl:if>
            <xsl:if test="following-sibling::*[1][self::legtab]">
                <xsl:copy-of select="following-sibling::*[1][self::legtab]"/>
            </xsl:if>
            <xsl:apply-templates select="node()|@*"/>
        </xsl:copy>
    </xsl:template>
    <xsl:template match="//titab[following-sibling::*[1][self::tab]]"/>
    <xsl:template match="//legtab[preceding-sibling::*[1][self::tab]]"/>
    
    <xsl:template match="//divintro">
        <xsl:copy>
            <xsl:apply-templates select="node()|@*"/>
            <xsl:if test="following-sibling::*[1][self::nb-bl]">
                <xsl:copy-of select="following-sibling::*[1][self::nb-bl]"/>
            </xsl:if>
        </xsl:copy>
    </xsl:template>
    <xsl:template match="//nb-bl[preceding-sibling::*[1][self::divintro]]"/>
    
    
    <xsl:template match="//sampletit"/>
    <xsl:template match="//auteur">
        <xsl:copy>
            <xsl:copy-of select="@*"/>
            <xsl:if test="matches(descendant::apimag/@fic, '[Ii][Nn][Vv][Ii][Ss][Ii][Bb][Ll][Ee]')">
                <xsl:attribute name="visible">non</xsl:attribute>
            </xsl:if>
            <xsl:text>&#x0A;</xsl:text>
        <xsl:apply-templates select="./imag"/>
            <xsl:text>&#x0A;</xsl:text>
        <xsl:apply-templates select="node()[name()!='imag']"/>
        </xsl:copy>
    </xsl:template>
    <xsl:template match="//sourcex">
        <xsl:choose>
            <xsl:when test="ancestor::comment">
                <xsl:copy>
                    <xsl:apply-templates/>
                </xsl:copy>
            </xsl:when>
            <xsl:otherwise>
                <xsl:apply-templates/>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>
    
	<xsl:template match="//prenom">
		<xsl:if test="parent::auteur/preceding-sibling::*[1][self::auteur]">
			<xsl:element name="introaut">et</xsl:element>
		</xsl:if>
		<xsl:copy>
			<xsl:apply-templates/>
		</xsl:copy>
    </xsl:template>
    
    <xsl:template match="observff[not(child::tiff or child::intertit or child::tab or child::cita-bl or child::imag)]">
        <xsl:copy>
            <al>
                <xsl:apply-templates select="node()|@*"/>
            </al>
        </xsl:copy>
    </xsl:template>
    <xsl:template match="pn[preceding-sibling::loc][not(child::tinoye)]">
        <p>
            <xsl:apply-templates select="node()|@*"/>
        </p>
    </xsl:template>
    
    <xsl:template match="//annexes">
        <xsl:choose>
            <xsl:when test="not(preceding-sibling::*[1][self::annexes]) and following-sibling::*[1][self::annexes]">
                <xsl:text disable-output-escaping="yes"><![CDATA[<annexes>]]></xsl:text>
                <xsl:apply-templates select="node()|@*"/>
            </xsl:when>
            <xsl:when test="preceding-sibling::*[1][self::annexes] and not(following-sibling::*[1][self::annexes])">
                <xsl:apply-templates select="node()|@*"/>
                <xsl:text disable-output-escaping="yes"><![CDATA[</annexes>]]></xsl:text>
            </xsl:when>
            <xsl:when test="not(preceding-sibling::*[1][self::annexes]) and not(following-sibling::*[1][self::annexes])">
                <xsl:text disable-output-escaping="yes"><![CDATA[<annexes>]]></xsl:text>
                <xsl:apply-templates select="node()|@*"/>
                <xsl:text disable-output-escaping="yes"><![CDATA[</annexes>]]></xsl:text>
            </xsl:when>
            <xsl:otherwise>
                <xsl:apply-templates select="node()|@*"/>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>
    <xsl:template match="//tbase">
        <xsl:choose>
            <xsl:when test="not(preceding-sibling::*[1][self::tbase]) and following-sibling::*[1][self::tbase]">
                <xsl:text disable-output-escaping="yes"><![CDATA[<tbase>]]></xsl:text>
                <xsl:apply-templates select="node()|@*"/>
            </xsl:when>
            <xsl:when test="preceding-sibling::*[1][self::tbase] and not(following-sibling::*[1][self::tbase])">
                <xsl:apply-templates select="node()|@*"/>
                <xsl:text disable-output-escaping="yes"><![CDATA[</tbase>]]></xsl:text>
            </xsl:when>
            <xsl:when test="not(preceding-sibling::*[1][self::tbase]) and not(following-sibling::*[1][self::tbase])">
                <xsl:text disable-output-escaping="yes"><![CDATA[<tbase>]]></xsl:text>
                <xsl:apply-templates select="node()|@*"/>
                <xsl:text disable-output-escaping="yes"><![CDATA[</tbase>]]></xsl:text>
            </xsl:when>
            <xsl:otherwise>
                <xsl:apply-templates select="node()|@*"/>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>
    
</xsl:stylesheet>