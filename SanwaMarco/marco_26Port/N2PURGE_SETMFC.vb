Function N2PURGE_GET_STS();
    SETVAR("@device", "N2Purge");

    'arg1. BAUD 鮑率設定(38400 Or 115200)
    'arg2. ID  站別(1~7)
	SETVAR("@cmd", "$1MCR:CFMFC:1,@<arg1>,@<arg2>");
    API("N2_PURGE_GET_CMD", True);
    RETVAL("@<N2_PURGE_GET_CMD>");'回傳最終結果
End Function;