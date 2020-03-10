
# What is Nova

> Nova is a programming language with a high level of abstraction. This repository contains a compiler in C# (.NET) and a virtual machine in C++ (iso 17)

  
  ###### Paradigms:
  + Compiled (into bytecode, no JIT for now)
  + Structured
  + Imperative
  + Functional
  + Strong typing
  ###### Example:

  ```
using <nova> // this is a nova library

// This code is written in Nova !
class MyClass
{
    public int Main()
    {
        Human human = -> Human("Freddy",18)
        human.PrintAge()
    }
}
struct Human
{
    public string Name
    public int Age
    
    -> Human(string name,int age) // Human constructor
    {
        Name = name
        Age = age
    }

    public void PrintAge()
    {
        Nova.PrintLine(Name + " is " + Age + " years old")
    }
}
 ```
 ###### How is the language working ?
 + Once your nova file(s) source code is written. Just give it to the C# compiler.
 + The compiler will generate a .nov file (sources to bytecode)
 + Give the .nov to the C++ virtual machine and it will run your program from Main() function.

###### Builder

 ![builder](https://puu.sh/F2jxl/e1f80ffc4a.png)

###### NOV File

![NOV Files](https://puu.sh/F2jDk/390c696ae5.png)

###### How to run
  + git clone https://github.com/Skinz3/Nova.git
  + Build Nova.Compiler & Nova.VM
  + ``` ./Nova.Compiler mySourceFile.nv output.nov ```  ---> myScript.nv must have a main point entry. 
  + ``` ./Nova.VM output.nov ``` ----> execute the program

## Thanks

  > Thanks to Uriopass (https://github.com/Uriopass) for his precious help
## Contacts

  > My discord is: **Skinz#1128**
  
