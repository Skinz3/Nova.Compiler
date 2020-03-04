#pragma once

enum OpCodes
{
	ArithmeticOp = 1,
	ComparaisonOp = 2,
	CtorCall = 3,
	Jump = 4,
	JumpIfFalse = 5,
	Load = 6,
	LoadStatic = 7,
	LoadStaticMember = 8,
	MethodCallMember = 9,
	MethodCallStatic = 10,
	Printl = 11,
	PushConst = 12,
	PushInt = 13,
	PushNull = 14,
	Readl = 15,
	Return = 16,
	Store = 17,
	StoreMember = 18,
	StoreStatic = 19,
	StructCall = 20,
	StructCreate = 21,
	StructLoadMember = 22,
	StructPushCurrent = 23,
	StructStoreMember = 24,
};