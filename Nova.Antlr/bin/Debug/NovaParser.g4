
parser grammar NovaParser;

options { tokenVocab=NovaLexer; }

compilationUnit
    : importDeclaration* typeDeclaration* EOF
    ;

importDeclaration
    : USING LT IDENTIFIER GT 
    ;

typeDeclaration
    : (classDeclaration | structDeclaration)
    | SEMI
    ;

classDeclaration
    : CLASS IDENTIFIER
      classBody
    ;

structDeclaration
    : STRUCT IDENTIFIER
      classBody
    ;

classBody
    : '{' (memberDeclaration)* '}'
    ;

memberDeclaration
    : modifier methodDeclaration
    | modifier fieldDeclaration
    | modifier constructorDeclaration
    ;

modifier
    : classModifier
    ;

classModifier 
    : PUBLIC
    | PRIVATE
    ;

fieldDeclaration
    : typeType variableDeclarator 
    ;

variableDeclarator
    : variableDeclaratorId (ASSIGN variableInitializer)?
    ;

variableInitializer
    : expression
    ;   

variableDeclaratorId
    : IDENTIFIER 
    ;

typeType
    : (IDENTIFIER | primitiveType) 
    ;

primitiveType
    : BOOLEAN
    | CHAR
    | BYTE
    | SHORT
    | INT
    | STRING
    | LONG
    | FLOAT
    | DOUBLE
    ;

expression
    : primary  #terminal 
    | expression bop='.' 
      ( IDENTIFIER
      | methodCall
      ) #terminal 
    | nativeCall #terminal 
    | methodCall #terminal 
    | NEW creator #terminal 
    | prefix=('+'|'-') expression #opExpr
    | left=expression bop=('*'|'/') right=expression #opExpr
    | left=expression bop=('+'|'-') right=expression #opExpr
    | left=expression ('<' '<' | '>' '>' '>' | '>' '>') right=expression #opExpr
    | left=expression bop=('<=' | '>=' | '>' | '<') right=expression #opExpr
    | left=expression bop=('==' | '!=') right=expression #opExpr
    | left=expression bop='&&' right=expression #opExpr
    | left=expression bop='||' right=expression #opExpr
    | <assoc=right> left=expression '=' right=expression #opExpr
    ;

methodDeclaration
    : typeTypeOrUnit IDENTIFIER formalParameters (LBRACK RBRACK)*
      methodBody
    ;

typeTypeOrUnit
    : typeType
    | UNIT
    ;


formalParameters
    : '(' formalParameterList? ')'
    ;

formalParameterList
    : formalParameter (',' formalParameter)* (',' lastFormalParameter)?
    | lastFormalParameter
    ;

formalParameter
    : typeType variableDeclaratorId
    ;

lastFormalParameter
    : typeType variableDeclaratorId
    ;

constructorDeclaration
    : '->' IDENTIFIER formalParameters constructorBody=block
    ;

methodBody
    : block
    ;

block
    : '{' statement* '}'
    ;

localVariableDeclaration
    : typeType variableDeclarator
    ;

statement
    : blockLabel=block
    | localVariableDeclaration 
    | ifStatement
    | forStatement
    | WHILE parExpression statement
    | RETURN expression? 
    | statementExpression
    ;

statementExpression
    : expression 
    ;
ifStatement
    :IF parExpression statement (ELSE statement)?
    ;

forStatement
    : 
    FOR '(' forControl ')' statement
    ;

parExpression
    : '(' expression ')'
    ;

forControl
    : forInit ';' forCond=expression ';' forUpdate=expressionList?
    ;

forInit
    : localVariableDeclaration
    | expressionList
    ;

expressionList
    : expression (COMMA expression)*
    ;

primary
    : '(' expression ')'
    | literal
    | IDENTIFIER
    ;

literal
    : integerLiteral
    | floatLiteral
    | CHAR_LITERAL
    | STRING_LITERAL
    | BOOL_LITERAL
    | NULL_LITERAL
    ;

integerLiteral
    : DECIMAL_LITERAL
    | HEX_LITERAL
    | OCT_LITERAL
    | BINARY_LITERAL
    ;

floatLiteral
    : FLOAT_LITERAL
    | HEX_FLOAT_LITERAL
    ;

methodCall
    : IDENTIFIER '(' expressionList? ')'
    ;

nativeCall
    : '~' IDENTIFIER '(' expressionList? ')'
    ;

creator
    : createdName (classCreatorRest)
    ;

createdName
    : IDENTIFIER 
    ;

classCreatorRest
    : arguments 
    ;

arguments
    : '(' expressionList? ')'
    ;

