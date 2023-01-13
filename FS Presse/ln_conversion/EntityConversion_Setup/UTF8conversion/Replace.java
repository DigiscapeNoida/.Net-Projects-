import java.io.*;
import java.util.*;
import java.lang.StringBuffer;

public class Replace {
	String inbuf="";
	byte outbuf[]=null;
	int outbufLength=0;
	boolean starter[]=new boolean[256];
	int maxLen=0;
    Properties entProp=null, utfProp=null;

	public Properties load(String arg) {
		Properties p=new Properties();
        try {
        	FileInputStream fs=new FileInputStream(new File(arg));
        	p.load(fs);
        	fs.close();
        } catch (Exception e) {
        	System.err.println("Failed to open Property file "+arg);
        	p=null;
        }
        return p;
	}

	public void read(String p1,String p2) {
		entProp=load(p2);
        Enumeration en = entProp.propertyNames();
        while(en.hasMoreElements()) {
            String name = (String)en.nextElement();
            if (maxLen<name.length()) maxLen=name.length();
        }

		String s=null;
		try {
		   File f1=new File(p1);
		   outbuf=new byte[2*(int)f1.length()];
		   BufferedReader in = new BufferedReader(new FileReader(p1));
		   String sep="";
		   while (((s=in.readLine())!=null)) {
		   	  inbuf=inbuf+sep+s;
		   	  sep="\n";
		   }
		   in.close();
		} catch (Exception e) {
			e.printStackTrace();
			inbuf=null;
			return;
		}
	}
	public void write(String arg) {
		try {
           FileOutputStream ps=new FileOutputStream(arg);
           ps.write(outbuf,0,outbufLength);
           ps.close();
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	public String utf8(int c) {
		char b1, b2, b3, b4;
		if (c<0x80)
			return ""+((char)c);
		else if (c<0x0800) {
			b1 = (char)((c>>6  & 0x1F | 0xC0)& 0xFF);
			b2 = (char)((c>>0  & 0x3F | 0x80)& 0xFF);
			return ""+b1+""+b2;
		} else if (c<0x010000) {
			b1 = (char)((c>>12 & 0x0F | 0xE0)& 0xFF);
			b2 = (char)((c>>6  & 0x3F | 0x80)& 0xFF);
			b3 = (char)((c>>0  & 0x3F | 0x80)& 0xFF);
			return ""+b1+""+b2+""+b3;
		} else if (c<0x110000) {
			b1 = (char)((c>>18 & 0x07 | 0xF0)& 0xFF);
			b2 = (char)((c>>12 & 0x3F | 0x80)& 0xFF);
			b3 = (char)((c>>6  & 0x3F | 0x80)& 0xFF);
			b4 = (char)((c>>0  & 0x3F | 0x80)& 0xFF);
			return ""+b1+""+b2+""+b3+""+b4;
		}
		return null;
	}

	public int check(int p1) {
		String srch="";
		String r=null;
		char ch=0;
		for (int i=p1;i<inbuf.length() && i<p1+maxLen;i++) {
			srch=srch+(ch=inbuf.charAt(i));
			if (ch==';') break;
		}
		if (ch==';') {
			r=entProp.getProperty(srch);
			if (r==null && srch.charAt(1)=='#' && srch.charAt(2)=='x') {
				r=utf8(Integer.parseInt(srch.substring(3,srch.length()-1),16));
			} if (r==null && srch.charAt(1)=='#') {
				r=utf8(Integer.parseInt(srch.substring(2,srch.length()-1)));
			}
		}
		if (r!=null) {
			for (int i=0;i<r.length();i++) outbuf[outbufLength++]=(byte)r.charAt(i);
			return p1+srch.length()-1;
		}
		outbuf[outbufLength++]=(byte)'&';
		return p1;
	}

	public void convert() {
		for (int i=0;i<inbuf.length();i++) {
			char ch=inbuf.charAt(i);
			if (ch=='&')
				i=check(i);
			else
			    outbuf[outbufLength++]=(byte)ch;
		}
	}

	public void process(String p1, String p2, String p3) {
		read(p1,p3);
		convert();
		write(p2);
	}
	public static void main(String arg[]) {
		new Replace().process(arg[0],arg[1],arg[2]);
//		new Replace().process("E:\\ppp_root\\JOBS\\elsevier\\journal\\colsua\\17351\\tx1.xml","tx1.out","Entities.ini");
	}
}
