
# What is Nova

> Nova is an high level abstraction interpreted language written in C++.
  
  ###### Paradigms:
  + Interpreted (for now)
  + Structured
  + Imperative
  + Object-oriented
  + Functional.

  ###### Example:

  ```
using nova
using nova.math

namespace myProgram

class MyClass
{
    // This code is written in Nova !
    int main()
    {
       Nova.print("hello world");

       int var1 = 2;
       int var2 = -7;
       
       Nova.print(var1 * Math.Abs(var2));
    }
}
 ```
 ###### How to run

 ``` ./builder myScript.nv myAssembly.nov ```  ---> myScript.nv must have a main point entry.
 ``` ./interpreter myAssembly.nov ```

## Contacts

  * You can contact me : Discord : Skinz#1128
  
