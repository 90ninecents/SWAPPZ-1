#if defined(__arm__)
.text
	.align 3
methods:
	.space 16
	.align 2
Lm_c:
System_Linq_Check_Source_object:

	.byte 13,192,160,225,128,64,45,233,13,112,160,225,0,89,45,233,8,208,77,226,13,176,160,225,0,0,139,229,0,0,155,229
	.byte 0,0,80,227,3,0,0,10,8,208,139,226,0,9,189,232,8,112,157,229,0,160,157,232,0,0,159,229,0,0,0,234
	.long mono_aot_System_Core_got - . -12
	.byte 0,0,159,231,1,16,160,227
bl p_1

	.byte 0,16,160,225,57,0,160,227,2,4,128,226
bl p_2
bl p_3

Lme_c:
	.align 2
Lm_d:
System_Linq_Check_SourceAndPredicate_object_object:

	.byte 13,192,160,225,128,64,45,233,13,112,160,225,0,89,45,233,8,208,77,226,13,176,160,225,0,0,139,229,4,16,139,229
	.byte 0,0,155,229,0,0,80,227,6,0,0,10,4,0,155,229,0,0,80,227,14,0,0,10,8,208,139,226,0,9,189,232
	.byte 8,112,157,229,0,160,157,232,0,0,159,229,0,0,0,234
	.long mono_aot_System_Core_got - . -12
	.byte 0,0,159,231,1,16,160,227
bl p_1

	.byte 0,16,160,225,57,0,160,227,2,4,128,226
bl p_2
bl p_3

	.byte 0,0,159,229,0,0,0,234
	.long mono_aot_System_Core_got - . -12
	.byte 0,0,159,231,15,16,160,227
bl p_1

	.byte 0,16,160,225,57,0,160,227,2,4,128,226
bl p_2
bl p_3

Lme_d:
	.align 2
Lm_e:
System_Linq_Enumerable_First_TSource_System_Collections_Generic_IEnumerable_1_TSource_System_Func_2_TSource_bool_System_Linq_Enumerable_Fallback:

	.byte 13,192,160,225,128,64,45,233,13,112,160,225,80,89,45,233,48,208,77,226,13,176,160,225,12,128,139,229,28,0,139,229
	.byte 1,96,160,225,32,32,139,229,0,0,160,227,0,0,139,229,12,0,155,229
bl p_4

	.byte 0,128,160,225,28,0,155,229,0,16,160,225,0,16,145,229,4,224,143,226,40,240,17,229,0,0,0,0,0,0,139,229
	.byte 22,0,0,234,0,0,155,229,40,0,139,229,12,0,155,229
bl p_5

	.byte 0,128,160,225,40,16,155,229,1,0,160,225,0,16,145,229,4,224,143,226,44,240,17,229,0,0,0,0,0,64,160,225
	.byte 12,0,155,229
bl p_6

	.byte 0,32,160,225,6,0,160,225,4,16,160,225,50,255,47,225,0,0,80,227,2,0,0,10,4,64,139,229,14,0,0,235
	.byte 38,0,0,234,0,16,155,229,1,0,160,225,0,16,145,229,0,128,159,229,0,0,0,234
	.long mono_aot_System_Core_got - . -4
	.byte 8,128,159,231,4,224,143,226,8,240,17,229,0,0,0,0,0,0,80,227,220,255,255,26,0,0,0,235,17,0,0,234
	.byte 24,224,139,229,0,0,155,229,0,0,80,227,1,0,0,26,24,192,155,229,12,240,160,225,0,16,155,229,1,0,160,225
	.byte 0,16,145,229,0,128,159,229,0,0,0,234
	.long mono_aot_System_Core_got - .
	.byte 8,128,159,231,4,224,143,226,32,240,17,229,0,0,0,0,24,192,155,229,12,240,160,225,32,0,155,229,1,0,80,227
	.byte 8,0,0,10,0,0,160,227,8,0,139,229,0,0,160,227,0,0,0,234,4,0,155,229,48,208,139,226,80,9,189,232
	.byte 8,112,157,229,0,160,157,232,225,0,160,227,2,4,128,226
bl p_7
bl p_3

Lme_e:
	.align 2
Lm_f:
System_Linq_Enumerable_FirstOrDefault_TSource_System_Collections_Generic_IEnumerable_1_TSource:

	.byte 13,192,160,225,128,64,45,233,13,112,160,225,0,89,45,233,16,208,77,226,13,176,160,225,0,128,139,229,4,0,139,229
	.byte 4,0,155,229
bl Lm_c

	.byte 0,0,155,229
bl p_8
bl p_9

	.byte 0,0,155,229
bl p_10

	.byte 0,0,144,229,8,0,139,229,0,0,155,229
bl p_11

	.byte 0,128,160,225,8,16,155,229,4,0,155,229,0,32,160,227
bl p_12

	.byte 16,208,139,226,0,9,189,232,8,112,157,229,0,160,157,232

Lme_f:
	.align 2
Lm_10:
System_Linq_Enumerable_Where_TSource_System_Collections_Generic_IEnumerable_1_TSource_System_Func_2_TSource_bool:

	.byte 13,192,160,225,128,64,45,233,13,112,160,225,0,89,45,233,16,208,77,226,13,176,160,225,0,128,139,229,4,0,139,229
	.byte 8,16,139,229,4,0,155,229,8,16,155,229
bl Lm_d

	.byte 0,0,155,229
bl p_13

	.byte 0,128,160,225,4,0,155,229,8,16,155,229
bl p_14

	.byte 16,208,139,226,0,9,189,232,8,112,157,229,0,160,157,232

Lme_10:
	.align 2
Lm_11:
System_Linq_Enumerable_CreateWhereIterator_TSource_System_Collections_Generic_IEnumerable_1_TSource_System_Func_2_TSource_bool:

	.byte 13,192,160,225,128,64,45,233,13,112,160,225,0,89,45,233,24,208,77,226,13,176,160,225,0,128,139,229,4,0,139,229
	.byte 8,16,139,229,0,0,155,229
bl p_15
bl p_16

	.byte 16,0,139,229
bl p_17

	.byte 16,16,155,229,1,0,160,225,4,48,155,229,8,48,129,229,8,32,155,229,20,32,129,229,28,48,129,229,32,32,129,229
	.byte 1,32,224,227,36,32,129,229,24,208,139,226,0,9,189,232,8,112,157,229,0,160,157,232

Lme_11:
	.align 2
Lm_12:
System_Linq_Enumerable__CreateWhereIteratorc__Iterator1D_1__ctor:

	.byte 13,192,160,225,128,64,45,233,13,112,160,225,0,89,45,233,8,208,77,226,13,176,160,225,0,0,139,229,8,208,139,226
	.byte 0,9,189,232,8,112,157,229,0,160,157,232

Lme_12:
	.align 2
Lm_13:
System_Linq_Enumerable__CreateWhereIteratorc__Iterator1D_1_System_Collections_Generic_IEnumerator_TSource_get_Current:

	.byte 13,192,160,225,128,64,45,233,13,112,160,225,0,89,45,233,8,208,77,226,13,176,160,225,0,0,139,229,0,0,155,229
	.byte 24,0,144,229,8,208,139,226,0,9,189,232,8,112,157,229,0,160,157,232

Lme_13:
	.align 2
Lm_14:
System_Linq_Enumerable__CreateWhereIteratorc__Iterator1D_1_System_Collections_IEnumerator_get_Current:

	.byte 13,192,160,225,128,64,45,233,13,112,160,225,0,89,45,233,8,208,77,226,13,176,160,225,0,0,139,229,0,0,155,229
	.byte 24,0,144,229,8,208,139,226,0,9,189,232,8,112,157,229,0,160,157,232

Lme_14:
	.align 2
Lm_15:
System_Linq_Enumerable__CreateWhereIteratorc__Iterator1D_1_System_Collections_IEnumerable_GetEnumerator:

	.byte 13,192,160,225,128,64,45,233,13,112,160,225,0,89,45,233,8,208,77,226,13,176,160,225,0,0,139,229,0,16,155,229
	.byte 1,0,160,225,0,224,145,229
bl Lm_16

	.byte 8,208,139,226,0,9,189,232,8,112,157,229,0,160,157,232

Lme_15:
	.align 2
Lm_16:
System_Linq_Enumerable__CreateWhereIteratorc__Iterator1D_1_System_Collections_Generic_IEnumerable_TSource_GetEnumerator:

	.byte 13,192,160,225,128,64,45,233,13,112,160,225,0,93,45,233,20,208,77,226,13,176,160,225,0,0,139,229,0,0,155,229
	.byte 36,0,128,226,0,16,160,227,1,32,224,227
bl p_18

	.byte 1,16,224,227,1,0,80,225,1,0,0,26,0,0,155,229,14,0,0,234,0,0,155,229,0,0,144,229
bl p_19
bl p_16

	.byte 8,0,139,229
bl Lm_12

	.byte 8,0,155,229,0,160,160,225,0,16,155,229,28,16,145,229,8,16,128,229,0,16,155,229,32,16,145,229,20,16,128,229
	.byte 10,0,160,225,20,208,139,226,0,13,189,232,8,112,157,229,0,160,157,232

Lme_16:
	.align 2
Lm_17:
System_Linq_Enumerable__CreateWhereIteratorc__Iterator1D_1_MoveNext:

	.byte 13,192,160,225,128,64,45,233,13,112,160,225,0,93,45,233,44,208,77,226,13,176,160,225,20,0,139,229,0,0,160,227
	.byte 0,0,203,229,20,0,155,229,36,160,144,229,20,0,155,229,0,16,224,227,36,16,128,229,0,0,160,227,0,0,203,229
	.byte 16,160,139,229,2,0,90,227,126,0,0,42,16,0,155,229,0,17,160,225,0,0,159,229,0,0,0,234
	.long mono_aot_System_Core_got - . + 4
	.byte 0,0,159,231,1,0,128,224,0,0,144,229,0,240,160,225,20,0,155,229,24,0,139,229,20,0,155,229,8,0,144,229
	.byte 28,0,139,229,20,0,155,229,0,0,144,229
bl p_20

	.byte 0,128,160,225,28,16,155,229,1,0,160,225,0,16,145,229,4,224,143,226,40,240,17,229,0,0,0,0,0,16,160,225
	.byte 24,0,155,229,12,16,128,229,2,160,224,227,1,160,74,226,1,0,90,227,7,0,0,42,10,17,160,225,0,0,159,229
	.byte 0,0,0,234
	.long mono_aot_System_Core_got - . + 8
	.byte 0,0,159,231,1,0,128,224,0,0,144,229,0,240,160,225,43,0,0,234,20,0,155,229,32,0,139,229,20,0,155,229
	.byte 12,0,144,229,36,0,139,229,20,0,155,229,0,0,144,229
bl p_21

	.byte 0,128,160,225,36,16,155,229,1,0,160,225,0,16,145,229,4,224,143,226,44,240,17,229,0,0,0,0,0,16,160,225
	.byte 32,0,155,229,16,16,128,229,20,0,155,229,20,0,144,229,24,0,139,229,20,0,155,229,16,0,144,229,28,0,139,229
	.byte 20,0,155,229,0,0,144,229
bl p_22

	.byte 0,32,160,225,24,0,155,229,28,16,155,229,50,255,47,225,0,0,80,227,10,0,0,10,20,0,155,229,0,16,160,225
	.byte 16,16,145,229,24,16,128,229,20,0,155,229,1,16,160,227,36,16,128,229,1,0,160,227,0,0,203,229,15,0,0,235
	.byte 44,0,0,234,20,0,155,229,12,16,144,229,1,0,160,225,0,16,145,229,0,128,159,229,0,0,0,234
	.long mono_aot_System_Core_got - . -4
	.byte 8,128,159,231,4,224,143,226,8,240,17,229,0,0,0,0,0,0,80,227,198,255,255,26,0,0,0,235,24,0,0,234
	.byte 12,224,139,229,0,0,219,229,0,0,80,227,1,0,0,10,12,192,155,229,12,240,160,225,20,0,155,229,12,0,144,229
	.byte 0,0,80,227,1,0,0,26,12,192,155,229,12,240,160,225,20,0,155,229,12,16,144,229,1,0,160,225,0,16,145,229
	.byte 0,128,159,229,0,0,0,234
	.long mono_aot_System_Core_got - .
	.byte 8,128,159,231,4,224,143,226,32,240,17,229,0,0,0,0,12,192,155,229,12,240,160,225,20,0,155,229,0,16,224,227
	.byte 36,16,128,229,0,0,160,227,0,0,0,234,1,0,160,227,44,208,139,226,0,13,189,232,8,112,157,229,0,160,157,232

Lme_17:
	.align 2
Lm_18:
System_Linq_Enumerable__CreateWhereIteratorc__Iterator1D_1_Dispose:

	.byte 13,192,160,225,128,64,45,233,13,112,160,225,0,89,45,233,24,208,77,226,13,176,160,225,16,0,139,229,16,0,155,229
	.byte 36,0,144,229,16,16,155,229,0,32,224,227,36,32,129,229,12,0,139,229,2,0,80,227,30,0,0,42,12,0,155,229
	.byte 0,17,160,225,0,0,159,229,0,0,0,234
	.long mono_aot_System_Core_got - . + 12
	.byte 0,0,159,231,1,0,128,224,0,0,144,229,0,240,160,225,0,0,0,235,19,0,0,234,8,224,139,229,16,0,155,229
	.byte 12,0,144,229,0,0,80,227,1,0,0,26,8,192,155,229,12,240,160,225,16,0,155,229,12,16,144,229,1,0,160,225
	.byte 0,16,145,229,0,128,159,229,0,0,0,234
	.long mono_aot_System_Core_got - .
	.byte 8,128,159,231,4,224,143,226,32,240,17,229,0,0,0,0,8,192,155,229,12,240,160,225,24,208,139,226,0,9,189,232
	.byte 8,112,157,229,0,160,157,232

Lme_18:
	.align 2
Lm_19:
System_Linq_Enumerable__CreateWhereIteratorc__Iterator1D_1_Reset:

	.byte 13,192,160,225,128,64,45,233,13,112,160,225,0,89,45,233,8,208,77,226,13,176,160,225,0,0,139,229,36,0,160,227
	.byte 1,12,128,226,2,4,128,226
bl p_7
bl p_3

	.byte 8,208,139,226,0,9,189,232,8,112,157,229,0,160,157,232

Lme_19:
	.align 2
Lm_1a:
System_Linq_Enumerable_PredicateOf_1__cctor:

	.byte 13,192,160,225,128,64,45,233,13,112,160,225,0,89,45,233,24,208,77,226,13,176,160,225,0,128,139,229,0,0,155,229
bl p_23
bl p_9

	.byte 0,0,155,229
bl p_24

	.byte 4,0,128,226,0,0,144,229,0,0,80,227,23,0,0,26,0,0,155,229
bl p_25
bl p_26

	.byte 16,0,139,229,0,0,155,229
bl p_27
bl p_16

	.byte 12,0,139,229,0,0,155,229
bl p_28

	.byte 0,48,160,225,12,0,155,229,16,32,155,229,8,0,139,229,0,16,160,227,51,255,47,225,0,0,155,229
bl p_23
bl p_9

	.byte 0,0,155,229
bl p_24

	.byte 8,16,155,229,4,0,128,226,0,16,128,229,0,0,155,229
bl p_23
bl p_9

	.byte 0,0,155,229
bl p_24

	.byte 4,0,128,226,0,0,144,229,8,0,139,229,0,0,155,229
bl p_23
bl p_9

	.byte 0,0,155,229
bl p_24

	.byte 8,16,155,229,0,16,128,229,24,208,139,226,0,9,189,232,8,112,157,229,0,160,157,232

Lme_1a:
	.align 2
Lm_1b:
System_Linq_Enumerable_PredicateOf_1__Alwaysm__7B_T:

	.byte 13,192,160,225,128,64,45,233,13,112,160,225,0,89,45,233,8,208,77,226,13,176,160,225,0,128,139,229,4,0,139,229
	.byte 1,0,160,227,8,208,139,226,0,9,189,232,8,112,157,229,0,160,157,232

Lme_1b:
	.align 2
Lm_1c:
System_Runtime_CompilerServices_ExtensionAttribute__ctor:

	.byte 13,192,160,225,128,64,45,233,13,112,160,225,0,89,45,233,8,208,77,226,13,176,160,225,0,0,139,229,8,208,139,226
	.byte 0,9,189,232,8,112,157,229,0,160,157,232

Lme_1c:
	.align 2
Lm_1f:
wrapper_delegate_invoke_System_Action_invoke_void__this__:

	.byte 13,192,160,225,128,64,45,233,13,112,160,225,96,93,45,233,4,208,77,226,13,176,160,225,0,160,160,225,0,0,159,229
	.byte 0,0,0,234
	.long mono_aot_System_Core_got - . + 16
	.byte 0,0,159,231,0,0,144,229,0,0,80,227,25,0,0,26,44,0,138,226,0,80,144,229,5,0,160,225,0,0,80,227
	.byte 16,0,0,26,16,0,138,226,0,96,144,229,6,0,160,225,0,0,80,227,4,0,0,10,8,0,138,226,0,16,144,229
	.byte 6,0,160,225,49,255,47,225,2,0,0,234,8,0,138,226,0,0,144,229,48,255,47,225,4,208,139,226,96,13,189,232
	.byte 8,112,157,229,0,160,157,232,5,0,160,225,15,224,160,225,12,240,149,229,234,255,255,234
bl p_29

	.byte 227,255,255,234

Lme_1f:
	.align 2
Lm_20:
wrapper_delegate_begin_invoke_System_Action_begin_invoke_IAsyncResult__this___AsyncCallback_object_System_AsyncCallback_object:

	.byte 13,192,160,225,128,64,45,233,13,112,160,225,0,89,45,233,16,208,77,226,13,176,160,225,0,0,139,229,4,16,139,229
	.byte 8,32,139,229,12,0,160,227,7,16,128,226,7,16,193,227,1,208,77,224,0,224,160,227,0,0,0,234,1,224,141,231
	.byte 4,16,81,226,252,255,255,170,0,16,141,226,1,0,160,225,4,32,139,226,0,32,129,229,4,0,128,226,8,32,139,226
	.byte 0,32,128,229,0,0,155,229
bl p_30

	.byte 16,208,139,226,0,9,189,232,8,112,157,229,0,160,157,232

Lme_20:
	.align 2
Lm_21:
wrapper_delegate_end_invoke_System_Action_end_invoke_void__this___IAsyncResult_System_IAsyncResult:

	.byte 13,192,160,225,128,64,45,233,13,112,160,225,0,89,45,233,8,208,77,226,13,176,160,225,0,0,139,229,4,16,139,229
	.byte 8,0,160,227,7,16,128,226,7,16,193,227,1,208,77,224,0,224,160,227,0,0,0,234,1,224,141,231,4,16,81,226
	.byte 252,255,255,170,0,16,141,226,4,0,139,226,0,0,129,229,0,0,155,229
bl p_31

	.byte 8,208,139,226,0,9,189,232,8,112,157,229,0,160,157,232

Lme_21:
.text
	.align 3
methods_end:
.text
	.align 3
method_offsets:

	.long -1,-1,-1,-1,-1,-1,-1,-1
	.long -1,-1,-1,-1,Lm_c - methods,Lm_d - methods,Lm_e - methods,Lm_f - methods
	.long Lm_10 - methods,Lm_11 - methods,Lm_12 - methods,Lm_13 - methods,Lm_14 - methods,Lm_15 - methods,Lm_16 - methods,Lm_17 - methods
	.long Lm_18 - methods,Lm_19 - methods,Lm_1a - methods,Lm_1b - methods,Lm_1c - methods,-1,-1,Lm_1f - methods
	.long Lm_20 - methods,Lm_21 - methods

.text
	.align 3
method_info:
mi:
Lm_c_p:

	.byte 0,0
Lm_d_p:

	.byte 0,0
Lm_e_p:

	.byte 0,2,2,3
Lm_f_p:

	.byte 0,0
Lm_10_p:

	.byte 0,0
Lm_11_p:

	.byte 0,0
Lm_12_p:

	.byte 0,0
Lm_13_p:

	.byte 0,0
Lm_14_p:

	.byte 0,0
Lm_15_p:

	.byte 0,0
Lm_16_p:

	.byte 0,0
Lm_17_p:

	.byte 0,4,4,5,2,3
Lm_18_p:

	.byte 0,2,6,3
Lm_19_p:

	.byte 0,0
Lm_1a_p:

	.byte 9,0,0
Lm_1b_p:

	.byte 9,0,0
Lm_1c_p:

	.byte 0,0
Lm_1f_p:

	.byte 0,1,7
Lm_20_p:

	.byte 0,0
Lm_21_p:

	.byte 0,0
.text
	.align 3
method_info_offsets:

	.long 0,0,0,0,0,0,0,0
	.long 0,0,0,0,Lm_c_p - mi,Lm_d_p - mi,Lm_e_p - mi,Lm_f_p - mi
	.long Lm_10_p - mi,Lm_11_p - mi,Lm_12_p - mi,Lm_13_p - mi,Lm_14_p - mi,Lm_15_p - mi,Lm_16_p - mi,Lm_17_p - mi
	.long Lm_18_p - mi,Lm_19_p - mi,Lm_1a_p - mi,Lm_1b_p - mi,Lm_1c_p - mi,0,0,Lm_1f_p - mi
	.long Lm_20_p - mi,Lm_21_p - mi

.text
	.align 3
extra_method_info:

	.byte 0,1,1,105,110,118,111,107,101,95,118,111,105,100,95,95,116,104,105,115,95,95,32,40,41,0,1,2,98,101,103,105
	.byte 110,95,105,110,118,111,107,101,95,73,65,115,121,110,99,82,101,115,117,108,116,95,95,116,104,105,115,95,95,95,65,115
	.byte 121,110,99,67,97,108,108,98,97,99,107,95,111,98,106,101,99,116,32,40,83,121,115,116,101,109,46,65,115,121,110,99
	.byte 67,97,108,108,98,97,99,107,44,111,98,106,101,99,116,41,0,1,3,101,110,100,95,105,110,118,111,107,101,95,118,111
	.byte 105,100,95,95,116,104,105,115,95,95,95,73,65,115,121,110,99,82,101,115,117,108,116,32,40,83,121,115,116,101,109,46
	.byte 73,65,115,121,110,99,82,101,115,117,108,116,41,0

.text
	.align 3
extra_method_table:

	.long 11,0,0,0,0,0,0,26
	.long 32,0,0,0,0,0,0,0
	.long 0,0,0,0,0,0,0,0
	.long 0,0,0,0,1,31,11,0
	.long 0,0,113,33,0
.text
	.align 3
extra_method_info_offsets:

	.long 4,30,0,31,1,32,26,33
	.long 113
.text
	.align 3
method_order:

	.long 12,16777215,12,13,14,15,16,17
	.long 18,19,20,21,22,23,24,25
	.long 26,27,28,31,32,33

.text
method_order_end:
.text
	.align 3
class_name_table:

	.short 19, 8, 0, 0, 0, 0, 0, 0
	.short 0, 0, 0, 0, 0, 1, 20, 7
	.short 0, 0, 0, 0, 0, 3, 0, 2
	.short 19, 0, 0, 0, 0, 9, 0, 0
	.short 0, 5, 0, 0, 0, 0, 0, 4
	.short 21, 6, 0, 10, 0
.text
	.align 3
got_info:

	.byte 12,0,39,6,193,0,4,69,6,193,0,8,193,8,2,112,128,188,8,1,129,156,8,2,128,184,96,33,7,12,104,101
	.byte 108,112,101,114,95,108,100,115,116,114,0,7,30,109,111,110,111,95,99,114,101,97,116,101,95,99,111,114,108,105,98,95
	.byte 101,120,99,101,112,116,105,111,110,95,49,0,7,25,109,111,110,111,95,97,114,99,104,95,116,104,114,111,119,95,101,120
	.byte 99,101,112,116,105,111,110,0,35,255,254,0,0,0,6,0,0,198,0,0,15,0,1,1,219,0,0,0,30,0,255,255
	.byte 255,255,255,15,2,5,6,255,254,0,0,0,219,0,0,0,21,107,1,1,219,0,0,0,30,0,255,255,255,255,255,15
	.byte 1,198,0,3,157,1,1,219,0,0,0,30,0,255,255,255,255,255,15,0,35,255,254,0,0,0,6,0,0,198,0,0
	.byte 15,0,1,1,219,0,0,0,30,0,255,255,255,255,255,15,2,5,6,255,254,0,0,0,219,0,0,0,21,108,1,1
	.byte 219,0,0,0,30,0,255,255,255,255,255,15,1,198,0,3,158,1,1,219,0,0,0,30,0,255,255,255,255,255,15,0
	.byte 35,255,254,0,0,0,6,0,0,198,0,0,15,0,1,1,219,0,0,0,30,0,255,255,255,255,255,15,2,6,6,255
	.byte 254,0,0,0,219,0,0,0,21,3,0,2,219,0,0,0,30,0,255,255,255,255,255,15,74,1,0,198,0,0,6,1
	.byte 2,219,0,0,0,30,0,255,255,255,255,255,15,74,1,0,7,30,109,111,110,111,95,99,114,101,97,116,101,95,99,111
	.byte 114,108,105,98,95,101,120,99,101,112,116,105,111,110,95,48,0,35,255,254,0,0,0,6,0,0,198,0,0,16,0,1
	.byte 1,219,0,0,0,30,0,255,255,255,255,255,16,2,2,11,219,0,0,0,21,9,0,1,219,0,0,0,30,0,255,255
	.byte 255,255,255,16,36,35,255,254,0,0,0,6,0,0,198,0,0,16,0,1,1,219,0,0,0,30,0,255,255,255,255,255
	.byte 16,2,0,11,219,0,0,0,21,9,0,1,219,0,0,0,30,0,255,255,255,255,255,16,35,255,254,0,0,0,6,0
	.byte 0,198,0,0,16,0,1,1,219,0,0,0,30,0,255,255,255,255,255,16,2,8,6,255,254,0,0,0,6,0,0,198
	.byte 0,0,15,0,1,1,219,0,0,0,30,0,255,255,255,255,255,16,3,255,254,0,0,0,6,0,0,198,0,0,15,0
	.byte 1,1,219,0,0,0,30,0,255,255,255,255,255,16,35,255,254,0,0,0,6,0,0,198,0,0,17,0,1,1,219,0
	.byte 0,0,30,0,255,255,255,255,255,17,2,8,6,255,254,0,0,0,6,0,0,198,0,0,18,0,1,1,219,0,0,0
	.byte 30,0,255,255,255,255,255,17,3,255,254,0,0,0,6,0,0,198,0,0,18,0,1,1,219,0,0,0,30,0,255,255
	.byte 255,255,255,17,35,255,254,0,0,0,6,0,0,198,0,0,18,0,1,1,219,0,0,0,30,0,255,255,255,255,255,18
	.byte 2,2,11,219,0,0,0,21,7,0,1,219,0,0,0,30,0,255,255,255,255,255,18,7,24,109,111,110,111,95,111,98
	.byte 106,101,99,116,95,110,101,119,95,115,112,101,99,105,102,105,99,0,3,255,254,0,0,0,219,0,0,0,21,7,0,1
	.byte 219,0,0,0,30,0,255,255,255,255,255,18,0,198,0,0,19,1,1,219,0,0,0,30,0,255,255,255,255,255,18,0
	.byte 3,193,0,19,152,35,255,254,0,0,0,7,0,0,198,0,0,23,1,1,219,0,0,0,19,0,0,7,0,0,0,2
	.byte 11,7,0,35,255,254,0,0,0,7,0,0,198,0,0,24,1,1,219,0,0,0,19,0,0,7,0,0,0,5,6,255
	.byte 254,0,0,0,219,0,0,0,21,107,1,1,219,0,0,0,19,0,0,7,0,1,198,0,3,157,1,1,219,0,0,0
	.byte 19,0,0,7,0,0,35,255,254,0,0,0,7,0,0,198,0,0,24,1,1,219,0,0,0,19,0,0,7,0,0,0
	.byte 5,6,255,254,0,0,0,219,0,0,0,21,108,1,1,219,0,0,0,19,0,0,7,0,1,198,0,3,158,1,1,219
	.byte 0,0,0,19,0,0,7,0,0,35,255,254,0,0,0,7,0,0,198,0,0,24,1,1,219,0,0,0,19,0,0,7
	.byte 0,0,0,6,6,255,254,0,0,0,219,0,0,0,21,3,0,2,219,0,0,0,19,0,0,7,0,74,1,0,198,0
	.byte 0,6,1,2,219,0,0,0,19,0,0,7,0,74,1,0,35,255,254,0,0,0,9,0,0,198,0,0,27,1,1,219
	.byte 0,0,0,19,0,0,9,0,0,0,2,11,9,0,35,255,254,0,0,0,9,0,0,198,0,0,27,1,1,219,0,0
	.byte 0,19,0,0,9,0,0,0,0,11,9,0,35,255,254,0,0,0,9,0,0,198,0,0,27,1,1,219,0,0,0,19
	.byte 0,0,9,0,0,0,5,6,28,7,10,109,111,110,111,95,108,100,102,116,110,0,35,255,254,0,0,0,9,0,0,198
	.byte 0,0,27,1,1,219,0,0,0,19,0,0,9,0,0,0,2,11,219,0,0,0,21,3,0,2,219,0,0,0,19,0
	.byte 0,9,0,74,1,35,255,254,0,0,0,9,0,0,198,0,0,27,1,1,219,0,0,0,19,0,0,9,0,0,0,6
	.byte 6,255,254,0,0,0,219,0,0,0,21,3,0,2,219,0,0,0,19,0,0,9,0,74,1,0,198,0,0,5,1,2
	.byte 219,0,0,0,19,0,0,9,0,74,1,0,7,35,109,111,110,111,95,116,104,114,101,97,100,95,105,110,116,101,114,114
	.byte 117,112,116,105,111,110,95,99,104,101,99,107,112,111,105,110,116,0,7,26,109,111,110,111,95,100,101,108,101,103,97,116
	.byte 101,95,98,101,103,105,110,95,105,110,118,111,107,101,0,7,24,109,111,110,111,95,100,101,108,101,103,97,116,101,95,101
	.byte 110,100,95,105,110,118,111,107,101,0
.text
	.align 3
got_info_offsets:

	.long 0,2,3,8,13,18,22,27
.text
	.align 3
ex_info:
ex:
Le_c_p:

	.byte 100,2,0,0
Le_d_p:

	.byte 128,160,2,0,0
Le_e_p:

	.byte 129,120,7,26,1,2,0,0,88,128,240,128,240,1,11,12,255,254,0,0,0,6,0,0,198,0,0,15,0,1,1,129
	.byte 41,1,0
Le_f_p:

	.byte 112,3,56,1,11,0,255,254,0,0,0,6,0,0,198,0,0,16,0,1,1,129,41,1,0
Le_10_p:

	.byte 88,3,56,1,11,0,255,254,0,0,0,6,0,0,198,0,0,17,0,1,1,129,41,1,0
Le_11_p:

	.byte 112,3,82,1,11,0,255,254,0,0,0,6,0,0,198,0,0,18,0,1,1,129,41,1,0
Le_12_p:

	.byte 44,3,0,1,11,0,255,254,0,0,0,219,0,0,0,21,7,0,1,129,41,1,0,198,0,0,19,1,1,129,41,1
	.byte 0,0
Le_13_p:

	.byte 52,3,0,1,11,0,255,254,0,0,0,219,0,0,0,21,7,0,1,129,41,1,0,198,0,0,20,1,1,129,41,1
	.byte 0,0
Le_14_p:

	.byte 52,3,0,1,11,0,255,254,0,0,0,219,0,0,0,21,7,0,1,129,41,1,0,198,0,0,21,1,1,129,41,1
	.byte 0,0
Le_15_p:

	.byte 60,3,0,1,11,0,255,254,0,0,0,219,0,0,0,21,7,0,1,129,41,1,0,198,0,0,22,1,1,129,41,1
	.byte 0,0
Le_16_p:

	.byte 128,144,3,108,1,11,0,255,254,0,0,0,219,0,0,0,21,7,0,1,129,41,1,0,198,0,0,23,1,1,129,41
	.byte 1,0,0
Le_17_p:

	.byte 130,100,7,128,136,1,2,0,0,128,188,129,216,129,216,1,11,20,255,254,0,0,0,219,0,0,0,21,7,0,1,129
	.byte 41,1,0,198,0,0,24,1,1,129,41,1,0,0
Le_18_p:

	.byte 128,200,7,82,1,2,0,0,96,104,104,1,11,16,255,254,0,0,0,219,0,0,0,21,7,0,1,129,41,1,0,198
	.byte 0,0,25,1,1,129,41,1,0,0
Le_19_p:

	.byte 64,3,0,1,11,0,255,254,0,0,0,219,0,0,0,21,7,0,1,129,41,1,0,198,0,0,26,1,1,129,41,1
	.byte 0,0
Le_1a_p:

	.byte 128,236,3,82,1,11,0,255,254,0,0,0,219,0,0,0,21,9,0,1,129,41,1,0,198,0,0,27,1,1,129,41
	.byte 1,0,0
Le_1b_p:

	.byte 52,3,0,1,11,0,255,254,0,0,0,219,0,0,0,21,9,0,1,129,41,1,0,198,0,0,28,1,1,129,41,1
	.byte 0,0
Le_1c_p:

	.byte 44,2,0,0
Le_1f_p:

	.byte 128,168,2,128,164,0
Le_20_p:

	.byte 124,2,56,0
Le_21_p:

	.byte 104,2,0,0
.text
	.align 3
ex_info_offsets:

	.long 0,0,0,0,0,0,0,0
	.long 0,0,0,0,Le_c_p - ex,Le_d_p - ex,Le_e_p - ex,Le_f_p - ex
	.long Le_10_p - ex,Le_11_p - ex,Le_12_p - ex,Le_13_p - ex,Le_14_p - ex,Le_15_p - ex,Le_16_p - ex,Le_17_p - ex
	.long Le_18_p - ex,Le_19_p - ex,Le_1a_p - ex,Le_1b_p - ex,Le_1c_p - ex,0,0,Le_1f_p - ex
	.long Le_20_p - ex,Le_21_p - ex

.text
	.align 3
unwind_info:

	.byte 25,12,13,0,76,14,8,135,2,68,14,24,136,6,139,5,140,4,142,3,68,14,32,68,13,11,29,12,13,0,76,14
	.byte 8,135,2,68,14,32,132,8,134,7,136,6,139,5,140,4,142,3,68,14,80,68,13,11,25,12,13,0,76,14,8,135
	.byte 2,68,14,24,136,6,139,5,140,4,142,3,68,14,40,68,13,11,25,12,13,0,76,14,8,135,2,68,14,24,136,6
	.byte 139,5,140,4,142,3,68,14,48,68,13,11,27,12,13,0,76,14,8,135,2,68,14,28,136,7,138,6,139,5,140,4
	.byte 142,3,68,14,48,68,13,11,27,12,13,0,76,14,8,135,2,68,14,28,136,7,138,6,139,5,140,4,142,3,68,14
	.byte 72,68,13,11,31,12,13,0,76,14,8,135,2,68,14,36,133,9,134,8,136,7,138,6,139,5,140,4,142,3,68,14
	.byte 40,68,13,11
.text
	.align 3
class_info:
LK_I_0:

	.byte 0,128,144,8,0,0,1
LK_I_1:

	.byte 14,128,160,52,0,0,4,193,0,11,129,193,0,10,247,193,0,11,125,193,0,10,246,193,0,6,221,193,0,10,245,193
	.byte 0,10,252,193,0,10,249,193,0,10,248,193,0,10,245,193,0,6,221,4,3,2
LK_I_2:

	.byte 255,255,255,255,255
LK_I_3:

	.byte 255,255,255,255,255
LK_I_4:

	.byte 4,128,144,8,0,0,1,193,0,11,129,193,0,11,126,193,0,11,125,193,0,11,123
LK_I_5:

	.byte 4,128,200,8,128,200,0,1,193,0,11,129,193,0,11,126,193,0,11,125,193,0,11,123
LK_I_6:

	.byte 255,255,255,255,255
LK_I_7:

	.byte 23,128,144,12,0,0,4,193,0,7,82,193,0,7,97,193,0,11,125,193,0,7,95,193,0,7,81,193,0,7,70,193
	.byte 0,7,55,193,0,7,56,193,0,7,57,193,0,7,58,193,0,7,59,193,0,7,60,193,0,7,61,193,0,7,62,193
	.byte 0,7,63,193,0,7,64,193,0,7,65,193,0,7,83,193,0,7,66,193,0,7,67,193,0,7,68,193,0,7,69,193
	.byte 0,7,85
LK_I_8:

	.byte 255,255,255,255,255
LK_I_9:

	.byte 4,128,144,8,0,0,1,193,0,11,129,193,0,2,20,193,0,11,125,193,0,2,25
.text
	.align 3
class_info_offsets:

	.long LK_I_0 - class_info,LK_I_1 - class_info,LK_I_2 - class_info,LK_I_3 - class_info,LK_I_4 - class_info,LK_I_5 - class_info,LK_I_6 - class_info,LK_I_7 - class_info
	.long LK_I_8 - class_info,LK_I_9 - class_info


.text
	.align 4
plt:
mono_aot_System_Core_plt:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_System_Core_got - . + 28,0
p_1:
plt__jit_icall_helper_ldstr:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_System_Core_got - . + 32,28
p_2:
plt__jit_icall_mono_create_corlib_exception_1:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_System_Core_got - . + 36,43
p_3:
plt__jit_icall_mono_arch_throw_exception:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_System_Core_got - . + 40,76
p_4:
plt__rgctx_fetch_0:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_System_Core_got - . + 44,104
p_5:
plt__rgctx_fetch_1:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_System_Core_got - . + 48,180
p_6:
plt__rgctx_fetch_2:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_System_Core_got - . + 52,256
p_7:
plt__jit_icall_mono_create_corlib_exception_0:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_System_Core_got - . + 56,336
p_8:
plt__rgctx_fetch_3:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_System_Core_got - . + 60,369
p_9:
plt__generic_class_init:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_System_Core_got - . + 64,420
p_10:
plt__rgctx_fetch_4:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_System_Core_got - . + 68,421
p_11:
plt__rgctx_fetch_5:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_System_Core_got - . + 72,472
p_12:
plt_System_Linq_Enumerable_First_TSource_System_Collections_Generic_IEnumerable_1_TSource_System_Func_2_TSource_bool_System_Linq_Enumerable_Fallback:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_System_Core_got - . + 76,530
p_13:
plt__rgctx_fetch_6:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_System_Core_got - . + 80,558
p_14:
plt_System_Linq_Enumerable_CreateWhereIterator_TSource_System_Collections_Generic_IEnumerable_1_TSource_System_Func_2_TSource_bool:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_System_Core_got - . + 84,616
p_15:
plt__rgctx_fetch_7:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_System_Core_got - . + 88,644
p_16:
plt__jit_icall_mono_object_new_specific:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_System_Core_got - . + 92,695
p_17:
plt_System_Linq_Enumerable__CreateWhereIteratorc__Iterator1D_1_TSource__ctor:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_System_Core_got - . + 96,722
p_18:
plt_System_Threading_Interlocked_CompareExchange_int__int_int:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_System_Core_got - . + 100,768
p_19:
plt__rgctx_fetch_8:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_System_Core_got - . + 104,773
p_20:
plt__rgctx_fetch_9:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_System_Core_got - . + 108,803
p_21:
plt__rgctx_fetch_10:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_System_Core_got - . + 112,870
p_22:
plt__rgctx_fetch_11:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_System_Core_got - . + 116,937
p_23:
plt__rgctx_fetch_12:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_System_Core_got - . + 120,1008
p_24:
plt__rgctx_fetch_13:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_System_Core_got - . + 124,1038
p_25:
plt__rgctx_fetch_14:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_System_Core_got - . + 128,1068
p_26:
plt__jit_icall_mono_ldftn:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_System_Core_got - . + 132,1097
p_27:
plt__rgctx_fetch_15:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_System_Core_got - . + 136,1110
p_28:
plt__rgctx_fetch_16:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_System_Core_got - . + 140,1157
p_29:
plt__jit_icall_mono_thread_interruption_checkpoint:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_System_Core_got - . + 144,1228
p_30:
plt__jit_icall_mono_delegate_begin_invoke:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_System_Core_got - . + 148,1266
p_31:
plt__jit_icall_mono_delegate_end_invoke:

	.byte 0,192,159,229,12,240,159,231
	.long mono_aot_System_Core_got - . + 152,1295
plt_end:
.text
	.align 3
mono_image_table:

	.long 2
	.asciz "System.Core"
	.asciz "E95670F9-346E-4FA5-B584-DB6ACCE3FC9F"
	.asciz ""
	.asciz "7cec85d7bea7798e"
	.align 3

	.long 1,2,0,5,0
	.asciz "mscorlib"
	.asciz "31EEB5C0-A469-4C24-B9DE-F145D69D8422"
	.asciz ""
	.asciz "7cec85d7bea7798e"
	.align 3

	.long 1,2,0,5,0
.data
	.align 3
mono_aot_System_Core_got:
	.space 160
got_end:
.data
	.align 3
mono_aot_got_addr:
	.align 2
	.long mono_aot_System_Core_got
.data
	.align 3
mono_aot_file_info:

	.long 8,160,32,34,1024,1024,128,0
	.long 0,0,0,0,0
.text
	.align 2
mono_assembly_guid:
	.asciz "E95670F9-346E-4FA5-B584-DB6ACCE3FC9F"
.text
	.align 2
mono_aot_version:
	.asciz "66"
.text
	.align 2
mono_aot_opt_flags:
	.asciz "55650815"
.text
	.align 2
mono_aot_full_aot:
	.asciz "TRUE"
.text
	.align 2
mono_runtime_version:
	.asciz ""
.text
	.align 2
mono_aot_assembly_name:
	.asciz "System.Core"
.text
	.align 3
Lglobals_hash:

	.short 73, 26, 0, 0, 0, 0, 0, 0
	.short 0, 14, 0, 18, 0, 0, 0, 0
	.short 0, 5, 0, 0, 0, 0, 0, 0
	.short 0, 0, 0, 0, 0, 0, 0, 28
	.short 0, 12, 0, 4, 0, 0, 0, 0
	.short 0, 3, 0, 27, 0, 0, 0, 8
	.short 0, 0, 0, 0, 0, 0, 0, 13
	.short 0, 1, 0, 0, 0, 0, 0, 11
	.short 74, 0, 0, 0, 0, 0, 0, 29
	.short 0, 2, 75, 0, 0, 0, 0, 0
	.short 0, 0, 0, 0, 0, 0, 0, 0
	.short 0, 21, 0, 0, 0, 0, 0, 0
	.short 0, 10, 0, 16, 0, 7, 0, 0
	.short 0, 0, 0, 0, 0, 0, 0, 0
	.short 0, 0, 0, 0, 0, 0, 0, 0
	.short 0, 0, 0, 0, 0, 15, 0, 19
	.short 0, 6, 73, 23, 0, 9, 0, 0
	.short 0, 0, 0, 0, 0, 0, 0, 0
	.short 0, 20, 0, 17, 76, 22, 0, 24
	.short 0, 25, 0
.text
	.align 2
name_0:
	.asciz "methods"
.text
	.align 2
name_1:
	.asciz "methods_end"
.text
	.align 2
name_2:
	.asciz "method_offsets"
.text
	.align 2
name_3:
	.asciz "method_info"
.text
	.align 2
name_4:
	.asciz "method_info_offsets"
.text
	.align 2
name_5:
	.asciz "extra_method_info"
.text
	.align 2
name_6:
	.asciz "extra_method_table"
.text
	.align 2
name_7:
	.asciz "extra_method_info_offsets"
.text
	.align 2
name_8:
	.asciz "method_order"
.text
	.align 2
name_9:
	.asciz "method_order_end"
.text
	.align 2
name_10:
	.asciz "class_name_table"
.text
	.align 2
name_11:
	.asciz "got_info"
.text
	.align 2
name_12:
	.asciz "got_info_offsets"
.text
	.align 2
name_13:
	.asciz "ex_info"
.text
	.align 2
name_14:
	.asciz "ex_info_offsets"
.text
	.align 2
name_15:
	.asciz "unwind_info"
.text
	.align 2
name_16:
	.asciz "class_info"
.text
	.align 2
name_17:
	.asciz "class_info_offsets"
.text
	.align 2
name_18:
	.asciz "plt"
.text
	.align 2
name_19:
	.asciz "plt_end"
.text
	.align 2
name_20:
	.asciz "mono_image_table"
.text
	.align 2
name_21:
	.asciz "mono_aot_got_addr"
.text
	.align 2
name_22:
	.asciz "mono_aot_file_info"
.text
	.align 2
name_23:
	.asciz "mono_assembly_guid"
.text
	.align 2
name_24:
	.asciz "mono_aot_version"
.text
	.align 2
name_25:
	.asciz "mono_aot_opt_flags"
.text
	.align 2
name_26:
	.asciz "mono_aot_full_aot"
.text
	.align 2
name_27:
	.asciz "mono_runtime_version"
.text
	.align 2
name_28:
	.asciz "mono_aot_assembly_name"
.data
	.align 3
Lglobals:
	.align 2
	.long Lglobals_hash
	.align 2
	.long name_0
	.align 2
	.long methods
	.align 2
	.long name_1
	.align 2
	.long methods_end
	.align 2
	.long name_2
	.align 2
	.long method_offsets
	.align 2
	.long name_3
	.align 2
	.long method_info
	.align 2
	.long name_4
	.align 2
	.long method_info_offsets
	.align 2
	.long name_5
	.align 2
	.long extra_method_info
	.align 2
	.long name_6
	.align 2
	.long extra_method_table
	.align 2
	.long name_7
	.align 2
	.long extra_method_info_offsets
	.align 2
	.long name_8
	.align 2
	.long method_order
	.align 2
	.long name_9
	.align 2
	.long method_order_end
	.align 2
	.long name_10
	.align 2
	.long class_name_table
	.align 2
	.long name_11
	.align 2
	.long got_info
	.align 2
	.long name_12
	.align 2
	.long got_info_offsets
	.align 2
	.long name_13
	.align 2
	.long ex_info
	.align 2
	.long name_14
	.align 2
	.long ex_info_offsets
	.align 2
	.long name_15
	.align 2
	.long unwind_info
	.align 2
	.long name_16
	.align 2
	.long class_info
	.align 2
	.long name_17
	.align 2
	.long class_info_offsets
	.align 2
	.long name_18
	.align 2
	.long plt
	.align 2
	.long name_19
	.align 2
	.long plt_end
	.align 2
	.long name_20
	.align 2
	.long mono_image_table
	.align 2
	.long name_21
	.align 2
	.long mono_aot_got_addr
	.align 2
	.long name_22
	.align 2
	.long mono_aot_file_info
	.align 2
	.long name_23
	.align 2
	.long mono_assembly_guid
	.align 2
	.long name_24
	.align 2
	.long mono_aot_version
	.align 2
	.long name_25
	.align 2
	.long mono_aot_opt_flags
	.align 2
	.long name_26
	.align 2
	.long mono_aot_full_aot
	.align 2
	.long name_27
	.align 2
	.long mono_runtime_version
	.align 2
	.long name_28
	.align 2
	.long mono_aot_assembly_name

	.long 0,0
	.globl _mono_aot_module_System_Core_info
	.align 3
_mono_aot_module_System_Core_info:
	.align 2
	.long Lglobals
.text
	.align 3
mem_end:
#endif
