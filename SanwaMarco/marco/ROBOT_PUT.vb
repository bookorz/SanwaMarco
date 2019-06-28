Function RobotPut();

    RETURN("Destination Empty","'@<arg1>' = 'undefined'");
    '設定點位(方便閱讀，拆成5行)
	SETVAR("@point",DECODE("@<arg1>","P1","0001","P2","0002","P3","0003","P4","0004","@<point>"));
	SETVAR("@point",DECODE("@<arg1>","BF11","0011","BF12","0012","BF13","0013","BF14","0014","BF15","0015","@<point>"));
	SETVAR("@point",DECODE("@<arg1>","BF21","0021","BF22","0022","BF23","0023","BF24","0024","BF25","0025","@<point>"));
	SETVAR("@point",DECODE("@<arg1>","BF31","0031","BF32","0032","BF33","0033","BF34","0034","@<point>"));
	SETVAR("@point",DECODE("@<arg1>","BF41","0041","BF42","0042","BF43","0043","BF44","0044","@<point>"));
	RETURN("point_unknow,@<arg1>","'@<point>' = 'undefined'");
	
	'檢查目的地在席 io Sensor
	SETVAR("@io",DECODE("@<arg1>","P1",101;102;103;2004;2005,"P2",201;202;203;2104;2105,"P3",301;302;303,"P4",309;310;311,"@<io>"));
	SETVAR("@io",DECODE("@<arg1>","BF11",500;501,"BF12",508;509,"BF13",600;601,"BF14",608;609,"BF15",808;809,"@<io>"));
	SETVAR("@io",DECODE("@<arg1>","BF21",502;503,"BF22",510;511,"BF23",602;603,"BF24",610;611,"BF25",810;811,"@<io>"));
	SETVAR("@io",DECODE("@<arg1>","BF31",504;505,"BF32",512;513,"BF33",604;605,"BF34",812;813,"@<io>"));
	SETVAR("@io",DECODE("@<arg1>","BF41",506;507,"BF42",514;515,"BF43",606;607,"BF44",814;815,"@<io>"));
	RETURN("sensor_unknow,@<arg1>","'@<io>' = 'undefined'");
	'檢查實際IO
	SETVAR("@interval", "100");
	SETVAR("@retry_count", "10");
	IF("'@<arg1>' in ('P1','P2')")
		SETVAR("@values", "11100");
	ELSEIF("'@<arg1>' in ('P3','P4')")
		SETVAR("@values", "111");
	ELSE
		SETVAR("@values", "11");
	ENDIF
	'RETURN("io sensors @<io> need value:@<values>");
	'檢查IO
    SETVAR("@device", "DNM01");
    API("I7565DNM_CHECK_IOS", True);
	
	'執行PUT
	SETVAR("@device", "Robot01");
	SETVAR("@cmd", "$1CMD:PUT__:@<point>");
    API("ATEL_ROBOT_MOTION_CMD", True);	
	
	'檢查實際IO
	SETVAR("@interval", "100");
	SETVAR("@retry_count", "10");
	IF("'@<arg1>' in ('P1','P2')")
		SETVAR("@values", "00000");
	ELSEIF("'@<arg1>' in ('P3','P4')")
		SETVAR("@values", "000");
	ELSE
		SETVAR("@values", "00");
	ENDIF
	'檢查IO
    SETVAR("@device", "DNM01");
    API("I7565DNM_CHECK_IOS", True);
	
End Function;