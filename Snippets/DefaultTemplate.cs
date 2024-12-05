using _M = System.Runtime.CompilerServices.MethodImplAttribute;

using System.Buffers.Text;
using System.Runtime.CompilerServices;
using System.Text;
using System.Numerics;

using var reader = new Reader();
using var writer = new Writer();

// Solution From Here.


#region FastIO
sealed class Reader : IDisposable {
const MethodImplOptions A=MethodImplOptions.AggressiveInlining;readonly Stream I=Console.OpenStandardInput();
byte[]B=new byte[1<<12];ulong[]D=new ulong[4];int P=0,L=0;bool S=false;Span<byte>F=>B.AsSpan(P,L-P);
public Reader()=>D[0]=0x1FFFFFFFF;
public Reader(int bufferSize):this()=>B=new byte[bufferSize];
[_M(A)]void SkipDelimiter(){while(P<L&&IsDelimiter(B[P]))P++;}
[_M(A)]bool IsDelimiter(byte c)=>(D[c/64]&(1UL<<(c%64)))>0;
[_M(A)]public void AddDelimiter(char c)=>D[c/64]|=1UL<<(c%64);
[_M(A)]public void RemoveDelimiter(char c)=>D[c/64]&=~(1UL<<(c%64));
[_M(A)]bool TokenReady(){for(int i=P;i<L;i++)if(IsDelimiter(B[i]))return true;return false;}
[_M(A)]void ReadToken(bool d=true){if(d)SkipDelimiter();if(S)return;if(P==L)P=L=0;if(!TokenReady()){var r=F.Length;F.CopyTo(B);P=0;L=r;}if(P==0){int r=I.Read(B,L,B.Length-L);if(r==0)S=true;L+=r;if(d)SkipDelimiter();}}
[_M(A)]public int NextInt(){ReadToken();if(!Utf8Parser.TryParse(F,out int v,out int b))throw new FormatException();P+=b;return v;}
[_M(A)]public long NextLong(){ReadToken();if(!Utf8Parser.TryParse(F,out long v,out int b))throw new FormatException();P+=b;return v;}
[_M(A)]public double NextDouble(){ReadToken();if(!Utf8Parser.TryParse(F,out double v,out int b))throw new FormatException();P+=b;return v;}
[_M(A)]public char NextChar(){ReadToken();return(char)B[P++];}
[_M(A)]public string NextString(){var s=new StringBuilder();ReadToken();while(true){if(P==L)ReadToken();if(IsDelimiter(B[P]))break;s.Append((char)B[P++]);}if(s.Length==0)return null;return s.ToString();}
[_M(A)]public string NextLine(){var s=new StringBuilder();ReadToken(false);while(true){if(P==L)ReadToken(false);if(B[P]=='\r'||B[P]=='\n'){if(B[P]=='\r'&&B[P + 1]=='\n')P++;P++;break;}s.Append((char)B[P++]);}if(s.Length==0)return null;return s.ToString();}
public bool IsStreamEnd()=>S&&P>=L;
public void Dispose()=>I.Dispose();
}

sealed class Writer : IDisposable
{
const MethodImplOptions AI=MethodImplOptions.AggressiveInlining;readonly Stream O=Console.OpenStandardOutput();
byte[]B=new byte[1<<12];int C=0;
public Writer(){}
public Writer(int bufferSize)=>B=new byte[bufferSize];
[_M(AI)]public void Write(string value){int c=0,l=value.Length;while(c<l){if(C==B.Length)Flush();B[C++]=(byte)value[c++];}}
[_M(AI)]public void Write(char value){if(C==B.Length)Flush();B[C++]=(byte)value;}
[_M(AI)]public void Write<T>(T value){Write(value.ToString());}
[_M(AI)]public void WriteLine(){Write('\n');}
[_M(AI)]public void WriteLine<T>(T value){Write(value);Write('\n');}
[_M(AI)]public void Flush(){O.Write(B.AsSpan(0, C));C=0;}
public void Dispose(){Flush();O.Flush();O.Dispose();}
}
#endregion