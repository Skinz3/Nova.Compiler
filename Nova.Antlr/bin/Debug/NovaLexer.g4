lexer grammar NovaLexer;


USING : 'using';

CLASS : 'class';
STRUCT : 'struct';

PUBLIC : 'public';
PRIVATE : 'private';

PRIMITIVE : 'primitive';

UNIT:               'unit';
DOUBLE:             'double';
FLOAT:              'float';
INT:                'int';
SHORT:              'short';
STRING:              'string';
BOOLEAN:            'bool';
CHAR:               'char';
BYTE:               'byte';
LONG:               'long';


FOR:                'for';
IF:                 'if';
WHILE:              'while';
ELSE:               'else';
RETURN:             'return';


            
NEW:                '->';

NATIVE:             '~';

// Operators

ASSIGN:             '=';
ADD:                '+';
SUB:                '-';
MUL:                '*';
DIV:                '/';
LE:                 '<=';
GE:                 '>=';
GT:                 '>';
LT:                 '<';
EQUAL:              '==';
NOTEQUAL:           '!=';
AND:                '&&';
OR:                 '||';

// Separators

LPAREN:             '(';
RPAREN:             ')';
LBRACE:             '{';
RBRACE:             '}';
LBRACK:             '[';
RBRACK:             ']';
COMMA:              ',';
DOT:                '.';
SEMI:               ';';

// Identifiers


NULL_LITERAL:       'null';

BOOL_LITERAL:       'true'
            |       'false'
            ;
IDENTIFIER:         Letter LetterOrDigit*;


// Fragment Rules

fragment HexDigits
    : HexDigit ((HexDigit | '_')* HexDigit)?
    ;
fragment HexDigit
    : [0-9a-fA-F]
    ;
fragment Digits
    : [0-9] ([0-9_]* [0-9])?
    ;
fragment LetterOrDigit
    : Letter
    | [0-9]
    ;
fragment Letter
    : [a-zA-Z$_] // these are the "java letters" below 0x7F
    | ~[\u0000-\u007F\uD800-\uDBFF] // covers all characters above 0x7F which are not a surrogate
    | [\uD800-\uDBFF] [\uDC00-\uDFFF] // covers UTF-16 surrogate pairs encodings for U+10000 to U+10FFFF
    ;

fragment EscapeSequence
    : '\\' [btnfr"'\\]
    | '\\' ([0-3]? [0-7])? [0-7]
    | '\\' 'u'+ HexDigit HexDigit HexDigit HexDigit
    ;

WS:                 [ \t\r\n\u000C]+ -> channel(HIDDEN);
COMMENT:            '/*' .*? '*/'    -> channel(HIDDEN);
LINE_COMMENT:       '//' ~[\r\n]*    -> channel(HIDDEN);



// Literals

DECIMAL_LITERAL:    ('0' | [1-9] (Digits? | '_'+ Digits)) [lL]?;
HEX_LITERAL:        '0' [xX] [0-9a-fA-F] ([0-9a-fA-F_]* [0-9a-fA-F])? [lL]?;
OCT_LITERAL:        '0' '_'* [0-7] ([0-7_]* [0-7])? [lL]?;
BINARY_LITERAL:     '0' [bB] [01] ([01_]* [01])? [lL]?;
                    
FLOAT_LITERAL:      (Digits '.' Digits? | '.' Digits) 
             ;

HEX_FLOAT_LITERAL:  '0' [xX] (HexDigits '.'? | HexDigits? '.' HexDigits) [pP] [+-]? Digits [fFdD]?;



CHAR_LITERAL:       '\'' (~['\\\r\n] | EscapeSequence) '\'';

STRING_LITERAL:     '"' (~["\\\r\n] | EscapeSequence)* '"';

