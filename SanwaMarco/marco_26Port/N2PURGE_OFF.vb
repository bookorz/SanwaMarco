Function N2PURGE_OFF();
    SETVAR("@device", "N2Purge");
    '<arg1> : PLATE 1~7: PLATE 位置選擇
    '       : STAGE 1~4: STAGE 選擇，一個 PLATE 最多有 4 個 STAGE(PLATE1 只有兩個)
    '11為第1個Stage Area第1個Plate
    '46為第4個Stage Area第6個Plate
	SETVAR("@cmd", "$1MCR:PGOFF:1,@<arg1>");
    API("N2_PURGE_SET_CMD", True);
End Function;