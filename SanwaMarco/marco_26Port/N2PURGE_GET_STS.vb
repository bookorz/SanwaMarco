Function N2PURGE_GET_STS();
    SETVAR("@device", "N2Purge");
    '<arg1> : PLATE 1~7: PLATE 位置選擇
    '<arg2> : STAGE 1~4: STAGE 選擇，一個 PLATE 最多有 4 個 STAGE(PLATE1 只有兩個)
	SETVAR("@cmd", "$1MCR:PGSTS:1,@<arg1>,@<arg2>");
    API("N2_PURGE_GET_CMD", True);
    RETVAL("@<N2_PURGE_GET_CMD>");'回傳最終結果
End Function;