
# What is Nova

> Nova is a cross plateform programming language. Nova compiler is written in C# .NET (Core is the goal) and virtual machine is written in C++ (ISO 17).

  
  ###### Paradigms:
  + Interpreted (only for now)
  + Structured
  + Imperative
  ###### Example:

  ```
using "nova.nov"

namespace myProgram

class MyClass
{
    // This code is written in Nova !
    public int Main()
    {
        Human human => ("Freddy",18)

        human.PrintAge()
    
    }
}
struct Human
{
    public string Name
    public int Age
    
    => Human(string name,int age)
    {
        Name = name
        Age = age
    }

    public void PrintAge()
    {
        Nova.PrintLine(Age)
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
  + Build Nova.Compiler & Nova.VM
  + ``` ./Nova.Compiler mySourceFile.nv output.nov ```  ---> myScript.nv must have a main point entry. 
  + ``` ./Nova.VM output.nov ``` ----> execute the program

## Contacts

  > My discord is: **Skinz#1128**
  
