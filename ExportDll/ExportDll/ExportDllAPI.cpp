#include "stdafx.h"
#include "ExportDllAPI.h"

/************************************************************************/
/*                          1. 调用约定                                   */
/************************************************************************/
//1.1 标准调用约定
EXPORTDLL_API void _stdcall CallingCvt_Stdcall()
{
	wprintf(L"CallingCvt_Stdcall\n");
}

//1.2 C调用约定
EXPORTDLL_API void _cdecl CallingCvt_Cdecl()
{
	wprintf(L"CallingCvt_Cdecl\n");
}

/************************************************************************/
/*                          2. 函数指针                                   */
/************************************************************************/
int PrintInt( int i )
{
	return(wprintf(L"CallBack_GetFunPtr %d\n", i));
}

EXPORTDLL_API PCALLBACKFUN CallBack_GetFunPtr()
{
	return(PrintInt);
}

/************************************************************************/
/*                          3. 字符串                                    */
/************************************************************************/
EXPORTDLL_API int Str_Output( WCHAR *pStr )
{
	if (NULL == pStr)
	{
		return(-1);
	}

	wprintf(L"Str_Output %s\n", pStr);

	return(0);
}

EXPORTDLL_API int Str_Change( WCHAR *pStr, int len )
{
	if (NULL == pStr)
	{
		return(-1);
	}

	for (int ix=0; ix<len-1; ix++)
	{
		pStr[ix] = 'a' + (ix) % 26;
	}
	pStr[len-1] = '\0\0';

	wprintf(L"Str_Change %s\n", pStr);
	return(0);
}

EXPORTDLL_API WCHAR * Str_Return()
{
	wprintf(L"Str_Return \n");

	return(g_StrReturn);
}

EXPORTDLL_API int Str_ChangeArr( WCHAR **ppStr, int len )
{
	if (NULL == ppStr)
	{
		return(-1);
	}

	for (int ix=0; ix<len; ix++)
	{
		if (NULL != ppStr[ix])
		{
			lstrcpyn(ppStr[ix], L"abc", 10);
		}
	}

	wprintf(L"Str_ChangeArr \n");
	return(0);
}

EXPORTDLL_API void Str_ParameterOut( WCHAR **ppStr )
{
	if (NULL == ppStr)
	{
		return;
	}

	*ppStr = (WCHAR *)CoTaskMemAlloc(128 * sizeof(WCHAR));

	lstrcpynW(*ppStr, L"abc", 128);
	wprintf(L"Str_ReturnOut \n");
}	

/************************************************************************/
/*                          4. 结构体                                    */
/************************************************************************/
EXPORTDLL_API void Struct_Change( testStru1 *pStru )
{
	if (NULL == pStru)
	{
		return;
	}

	pStru->iVal = 1;
	pStru->cVal = 'a';
	pStru->llVal = 2;

	wprintf(L"Struct_Change \n");
}

EXPORTDLL_API void Struct_PackN( testStru2 *pStru )
{
	if (NULL == pStru)
	{
		return;
	}

	pStru->iVal = 1;
	pStru->cVal = 'a';
	pStru->llVal = 2;

	wprintf(L"Struct_PackN \n");
}

EXPORTDLL_API void Struct_ChangeArr( testStru3 *pStru )
{
	if (NULL == pStru)
	{
		return;
	}

	pStru->iValArrp[0] = 8;
	lstrcpynW(pStru->szChArr, L"as", 30);

	wprintf(L"Struct_ChangeArr \n");
}

EXPORTDLL_API void Struct_Union( testStru4 *pStru )
{
	if (NULL == pStru)
	{
		return;
	}
	
	pStru->llLocation = 1024;
	wprintf(L"Struct_Union \n");
}

EXPORTDLL_API testStru5* Struct_Return()
{	
	g_stru5.iVal = 5;
	wprintf(L"Struct_Return \n");
	return(&g_stru5);
}

EXPORTDLL_API void Struct_StruArr( testStru6 *pStru, int len )
{
	if (NULL == pStru)
	{
		return;
	}

	for ( int ix=0; ix<len; ix++)
	{
		pStru[ix].iVal = ix;
	}

	wprintf(L"Struct_StruArr \n");
}

EXPORTDLL_API void Struct_Change2DArr( testStru7 *pStru )
{
	if (NULL == pStru)
	{
		return;
	}

	pStru->m[3][3] = 1;
	wprintf(L"Struct_Change2DArr \n");
}

EXPORTDLL_API void Struct_ParameterOut( testStru8 **ppStru )
{
	if (NULL == ppStru)
	{
		return;
	}

	*ppStru = (testStru8*)CoTaskMemAlloc(sizeof(testStru8));

	(*ppStru)->m	= 8;
	wprintf(L"Struct_ParameterOut \n");
}

EXPORTDLL_API void Struct_ChangePtr( testStru9 *pStru )
{
	if (NULL == pStru)
	{
		return;
	}

	pStru->IsBOOL = true;
	pStru->IsBOOL = TRUE;
	pStru->pWChArr = (WCHAR*)CoTaskMemAlloc(8*sizeof(WCHAR));
	pStru->pChArr = (CHAR*)CoTaskMemAlloc(8*sizeof(CHAR));

	lstrcpynW(pStru->pWChArr, L"ghj", 8);
	lstrcpynA(pStru->pChArr, "ghj", 8);

	wprintf(L"Struct_ChangePtr \n");
}

EXPORTDLL_API void Struct_NestStruct( testStru10 *pStru )
{
	if (NULL == pStru)
	{
		return;
	}

	pStru->lVal = 10;
	if (NULL != pStru->pPre)
	{
		pStru->pPre->iVal = 9;
	}

	wprintf(L"Struct_NestStruct \n");
}

EXPORTDLL_API void Err_Get()
{
	SetLastError(1010); //注册表项无效的错误码

	wprintf(L"Err_Get \n");
}





