Function RobotPut() AS String;

    SETVAR("@dest","SHELF45");'DEBUG:設定值測試
    '檢查參數,目的地不存在時, return NAK 
    RETURN("NAK:dest_empty","'@<dest>' = 'undefined'");
    '設定點位(方便閱讀，拆成5行)
	SETVAR("@point",DECODE("@<dest>","ELPT1",1,"ELPT2",2,"ILPT1",3,"ILPT2",4,"@<point>"));
	SETVAR("@point",DECODE("@<dest>","SHELF11",11,"SHELF12",12,"SHELF13",13,"SHELF14",14,"SHELF15",15,"@<point>"));
	SETVAR("@point",DECODE("@<dest>","SHELF21",21,"SHELF22",22,"SHELF23",23,"SHELF24",24,"SHELF25",25,"@<point>"));
	SETVAR("@point",DECODE("@<dest>","SHELF31",31,"SHELF32",32,"SHELF33",33,"SHELF34",34,"@<point>"));
	SETVAR("@point",DECODE("@<dest>","SHELF41",41,"SHELF42",42,"SHELF43",43,"SHELF44",44,"@<point>"));
	RETURN("NAK:point_unknow,@<dest>","'@<point>' = 'undefined'");
	
	SETVAR("@controller","$1");

    IF("'@dest' = 'ELPT1'")
        IF("'@ShutterState' != 'Open'")
            RETURN("ERR:The shutter 1 is not open yet");
        ENDIF
    ENDIF
    IF("'@dest' = 'ELPT2'")
        IF("'@ShutterState' != 'Open'")
            RETURN("ERR:The shutter 2 is not open yet");
        ENDIF
    ENDIF
    API("CheckPosition", true, "ERR:99999999");
    API("CheckPresence", true, "ERR:99999999");
    API("Put", true, "ERR:99999999");

End Function;