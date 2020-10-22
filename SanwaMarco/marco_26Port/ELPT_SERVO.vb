Function ELPT_MOVEIN();
    SETVAR("@device", "DNM01");
    'SET SERVO ON or OFF 
    SETVAR("@io", DECODE("@<arg1>", "P1", "2009", "P2", "2109", "@<io>"));
    SETVAR("@value", "@<arg2>");
    API("I7565DNM_SETIOS", True);
End Function;