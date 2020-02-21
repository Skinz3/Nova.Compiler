
# What is Nova

> Nova is a cross plateform programming language. Nova compiler is written in C# and virtual machine is written in C++.

  
  ###### Paradigms:
  + Interpreted (only for now)
  + Structured
  + Imperative
  ###### Example:

  ```
using "nova.nov"
using "nova_math.nov"

namespace myProgram

class MyClass
{
    // This code is written in Nova !
    public static int Main()
    {
       Nova.PrintLine("hello world")

       int var1 = 2
       int var2 = -7

       Nova.PrintLine(var1 * Math.Abs(var2))
    }
}
 ```
 ###### How is the language working ?
 + Once your nova file(s) source code is written. Just give it to the C# compiler.
 + The compiler will generate a .nov file (bytecode of the program)
 + Give the .nov to the virtual machine (written in C++) and it will run your program from Main() (entry point )

###### Builder

 ![builder](https://puu.sh/F2jxl/e1f80ffc4a.png)

###### NOV File

![NOV Files](https://puu.sh/F2jDk/390c696ae5.png)

###### How to run
  + git clone https://github.com/Skinz3/Nova.git
  + make ./Builder
  + make ./Interpreter
  + ``` ./builder myScript.nv myAssembly.nov ```  ---> myScript.nv must have a main point entry.
  + ``` ./interpreter myAssembly.nov ```

## Contacts

  > My discord is: **Skinz#1128**
  
