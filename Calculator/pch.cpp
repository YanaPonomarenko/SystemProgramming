// pch.cpp: source file corresponding to the pre-compiled header

#include "pch.h"

// When you are using pre-compiled headers, this source file is necessary for compilation to succeed.
#include "Calculator.h"
extern "C" {
	__declspec(dllexport) Calculator* CreateCalculatorObject()
	{
		return new Calculator();
	}

	__declspec(dllexport) void DeleteCalculatorObject(Calculator* obj)
	{
		delete obj;
	}

	__declspec(dllexport) int Add(Calculator* obj, int a, int b)
	{
		if (obj != nullptr)
		{
			return obj-> Add(a,b);
		}
		throw "object is nullptr";
	}
}