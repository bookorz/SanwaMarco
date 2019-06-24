Function RobotCarry() AS String;

    '檢查參數,目的地不存在時, return NAK 
    RETURN("NAK:dest_empty","'@destA' = ''");
    RETURN("NAK:dest_unknow","'@destA' NOT IN ('SHELF1','SHELF2','SHELF3','SHELF4','SHELF5','SHELF6','SHELF7','SHELF8','SHELF9','SHELF11','SHELF12','SHELF13','SHELF14','SHELF15','SHELF16','ILPT1','ILPT2','ELPT1','ELPT2')");
    RETURN("NAK:dest_empty","'@destB' = ''");
    RETURN("NAK:dest_unknow","'@destB' NOT IN ('SHELF1','SHELF2','SHELF3','SHELF4','SHELF5','SHELF6','SHELF7','SHELF8','SHELF9','SHELF11','SHELF12','SHELF13','SHELF14','SHELF15','SHELF16','ILPT1','ILPT2','ELPT1','ELPT2')");
    'Set Controller
	SETVAR("@controller","$1");
    SETVAR("@psCheckDelay",500);'訊號最大延遲時間，單位毫秒

    IF("'@destA' = 'ELPT1'")
        IF("'@ShutterState' != 'Open'")
            RETURN("ERR:The shutter 1 is not open yet");
        ENDIF
    ENDIF
    IF("'@destA' = 'ELPT2'")
        IF("'@ShutterState' != 'Open'")
            RETURN("ERR:The shutter 2 is not open yet");
        ENDIF
    ENDIF
    API("CheckPosition", true, "ERR:99999999");
    API("CheckPresence", true, "ERR:99999999");
    API("Get", true, "ERR:99999999");

    API("MoveAtoB", true, "ERR:99999999");

    IF("'@destB' = 'ELPT1'")
        IF("'@ShutterState' != 'Open'")
            RETURN("ERR:The shutter 1 is not open yet");
        ENDIF
    ENDIF
    IF("'@destB' = 'ELPT2'")
        IF("'@ShutterState' != 'Open'")
            RETURN("ERR:The shutter 2 is not open yet");
        ENDIF
    ENDIF
    API("CheckPosition", true, "ERR:99999999");
    API("CheckPresence", true, "ERR:99999999");
    API("Put", true, "ERR:99999999");

End Function;