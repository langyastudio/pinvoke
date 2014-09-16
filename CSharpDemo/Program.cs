using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CSharpDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //1.1 标准调用约定
            CExportDll.CallingCvt_Stdcall();

            //1.2 C调用约定
            CExportDll.CallingCvt_Cdecl();


            //2.1 获取回调函数的函数地址
            CExportDll.DelegateGetFunPtrType printInt = CExportDll.CallBack_GetFunPtr();
            printInt(100);


            //3.1 字符串作为输入参数
            string str = "hjkl;";
            CExportDll.Str_Output(str);

            //3.2 字符串作为参数,需要进行修改返回
            StringBuilder strBuilder = new StringBuilder(256);
            CExportDll.Str_Change(strBuilder, 256);

            //3.3 字符串作为返回值
            IntPtr strPtr = CExportDll.Str_Return();
            string strIntPtr = Marshal.PtrToStringUni(strPtr);

            //3.4 字符串数组作为参数,每个元素长度为10
            string[] strArr = new string[4] {new string('\0', 10), 
                                             new string('\0', 10),  
                                             new string('\0', 10), 
                                             new string('\0', 10) };
            CExportDll.Str_ChangeArr(strArr, 4);

            //3.5 释放非托管的内存,128
            string strOut = "";
            CExportDll.Str_ParameterOutString(ref strOut);
            //手动释放
            IntPtr strOutIntPtr = IntPtr.Zero;
            CExportDll.Str_ParameterOuttPtr(ref  strOutIntPtr);
            string strOut2 = Marshal.PtrToStringUni(strOutIntPtr);
            Marshal.FreeCoTaskMem(strOutIntPtr);


            //4.1 结构体作为输入输出参数
            CExportDll.testStru1 stru1 = new CExportDll.testStru1();
            CExportDll.Struct_Change(ref stru1);

            //4.2 结构体边界对齐
            CExportDll.testStru2 stru2 = new CExportDll.testStru2();
            CExportDll.Struct_PackN(ref stru2);

            //4.3 结构体中含有内置数据类型的数组
            CExportDll.testStru3 stru3 = new CExportDll.testStru3();
            CExportDll.Struct_ChangeArr(ref stru3);

            //4.4 union类型中含有结构体
            CExportDll.testStru4 stru4 = new CExportDll.testStru4();
            CExportDll.Struct_Union(ref stru4);

            //4.5 结构体作为返回值
            IntPtr struIntPtr = CExportDll.Struct_Return();
            CExportDll.testStru5 stru5 = (CExportDll.testStru5)(Marshal.PtrToStructure(struIntPtr, typeof(CExportDll.testStru5)));

            //4.6 结构体数组作为参数
            CExportDll.testStru6 []stru6 = new CExportDll.testStru6[5];
            CExportDll.Struct_StruArr(stru6, 5);

            //4.7 结构体中含有内置数据类型的二维数组
            CExportDll.testStru7 stru7 = new CExportDll.testStru7();
            CExportDll.Struct_Change2DArr(ref stru7);

            //4.8 结构体作为返出参数，释放非托管的内存
            IntPtr outPtr = IntPtr.Zero;
            CExportDll.Struct_ParameterOut(ref outPtr);
            CExportDll.testStru8 stru8 = (CExportDll.testStru8)(Marshal.PtrToStructure(outPtr, typeof(CExportDll.testStru8)));
            Marshal.FreeCoTaskMem(outPtr);

            //4.9 结构体中含有字符串--指针
            CExportDll.testStru9 stru9 = new CExportDll.testStru9();
            CExportDll.Struct_ChangePtr(ref stru9);

            //4.10 结构体中嵌套结构体指针
            CExportDll.testStru10Pre str10Pre = new CExportDll.testStru10Pre();
            IntPtr intPtrStru10Pre = Marshal.AllocCoTaskMem(Marshal.SizeOf(str10Pre));
            Marshal.StructureToPtr(str10Pre, intPtrStru10Pre, false);

            CExportDll.testStru10 stru10 = new CExportDll.testStru10();
            stru10.pPre = intPtrStru10Pre;
            CExportDll.Struct_NestStruct(ref stru10);
            CExportDll.testStru10Pre str10Pre2 = (CExportDll.testStru10Pre)Marshal.PtrToStructure(stru10.pPre, typeof(CExportDll.testStru10Pre));
           
            Marshal.DestroyStructure(intPtrStru10Pre, typeof(CExportDll.testStru10Pre));


            //5.1 获取错误码
            CExportDll.Err_Get();
            Win32Exception win32Exp = new Win32Exception();
            Console.WriteLine(win32Exp.Message);


            Console.ReadLine();
        }

 





    }
}
