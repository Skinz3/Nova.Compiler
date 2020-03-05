#pragma once

enum OpCodes
{
	Add = 1,
	Comparaison = 2,
	CtorCall = 3,
	Div = 4,
	Jump = 5,
	JumpIfFalse = 6,
	Load = 7,
	LoadStatic = 8,
	LoadStaticMember = 9,
	MethodCallMember = 10,
	MethodCallStatic = 11,
	Mul = 12,
	Printl = 13,
	PushConst = 14,
	PushInt = 15,
	PushNull = 16,
	Readl = 17,
	Return = 18,
	Store = 19,
	StoreMember = 20,
	StoreStatic = 21,
	StructCallMethod = 22,
	StructCreate = 23,
	StructLoadMember = 24,
	StructPushCurrent = 25,
	StructStoreMember = 26,
	Sub = 27
};