using System;

namespace SimpleCrud.Types {
    public  class Types {
        public static readonly   StringType STRING_TYPE = new StringType();
        public static readonly   SequenceType SEQUENCE_TYPE = new SequenceType();
        public static readonly   LongType LONG_TYPE = new LongType();
        public static readonly   IntType INT_TYPE = new IntType();
        public static readonly   DoubleType DOUBLE_TYPE = new DoubleType();
        public static readonly   DecimalType DECIMAL_TYPE = new DecimalType();
        public static readonly   DateTimeType DATE_TIME = new DateTimeType();
        public static readonly   BooleanType BOOLEAN_TYPE = new BooleanType();
        public static readonly   BooleanStringType BOOLEAN_STRING_TYPE = new BooleanStringType();
        public static readonly   BooleanIntType BOOLEAN_INT_TYPE = new BooleanIntType();
        public static readonly   AutoIncrementType AUTO_INCREMENT_TYPE = new AutoIncrementType();
    }
}
