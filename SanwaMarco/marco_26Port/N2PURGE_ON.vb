Function N2PURGE_ON();
    SETVAR("@device", "N2Purge");
    '<arg1> : PLATE 1~7: PLATE 位置選擇
    '       : STAGE 1~4: STAGE 選擇，一個 PLATE 最多有 4 個 STAGE(PLATE1 只有兩個)
    '11為第1個Stage Area第1個Plate
    '46為第4個Stage Area第6個Plate
    '<arg2>: MFC 1~100: Purge 流量控制
    '<arg3>: TIME 0: 不指定時間(維持開啟狀態), 1~10000: 在指定時間後自動關閉(秒)"
	SETVAR("@cmd", "$1MCR:PGON_:1,@<arg1>,@<arg2>,@<arg3>");
    API("N2_PURGE_SET_CMD", True);
End Function;