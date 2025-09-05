using System.Runtime.CompilerServices;

using Fahrenheit.Core;

namespace Fahrenheit.Tools.Atelier;

public enum AtelOp : byte {
    NOP        = 0x00,
    LOR        = 0x01,
    LAND       = 0x02,
    OR         = 0x03,
    EOR        = 0x04,
    AND        = 0x05,
    EQ         = 0x06,
    NE         = 0x07,
    GTU        = 0x08,
    LSU        = 0x09,
    GT         = 0x0A,
    LS         = 0x0B,
    GTEU       = 0x0C,
    LSEU       = 0x0D,
    GTE        = 0x0E,
    LSE        = 0x0F,
    BON        = 0x10,
    BOFF       = 0x11,
    SLL        = 0x12,
    SRL        = 0x13,
    ADD        = 0x14,
    SUB        = 0x15,
    MUL        = 0x16,
    DIV        = 0x17,
    MOD        = 0x18,
    NOT        = 0x19,
    UMINUS     = 0x1A,
    FIXADRS    = 0x1B,
    BNOT       = 0x1C,
    LABEL      = 0x1D, // imm u16
    TAG        = 0x1E, // imm u16
    PUSHV      = 0x1F, // imm u16
    POPV       = 0x20, // imm u16
    POPVL      = 0x21, // imm u16
    PUSHAR     = 0x22, // imm u16
    POPAR      = 0x23, // imm u16
    POPARL     = 0x24, // imm u16
    POPA       = 0x25,
    PUSHA      = 0x26,
    PUSHARP    = 0x27, // imm u16
    PUSHX      = 0x28,
    PUSHY      = 0x29,
    POPX       = 0x2A,
    REPUSH     = 0x2B,
    POPY       = 0x2C,
    PUSHI      = 0x2D, // imm u16
    PUSHII     = 0x2E, // imm s16
    PUSHF      = 0x2F, // imm u16
    JMP        = 0x30, // imm u16
    CJMP       = 0x31, // imm u16
    NCJMP      = 0x32, // imm u16
    JSR        = 0x33, // imm u16
    RTS        = 0x34,
    CALL       = 0x35, // imm u16
    REQ        = 0x36,
    REQSW      = 0x37,
    REQEW      = 0x38,
    PREQ       = 0x39,
    PREQSW     = 0x3A,
    PREQEW     = 0x3B,
    RET        = 0x3C,
    RETN       = 0x3D,
    RETT       = 0x3E,
    RETTN      = 0x3F,
    HALT       = 0x40,
    PUSHN      = 0x41, // imm u16
    PUSHT      = 0x42, // imm u16
    PUSHVP     = 0x43, // imm u16
    PUSHFIX    = 0x44, // imm u16
    FREQ       = 0x45,
    TREQ       = 0x46,
    BREQ       = 0x47,
    BFREQ      = 0x48,
    BTREQ      = 0x49,
    FREQSW     = 0x4A,
    TREQSW     = 0x4B,
    BREQSW     = 0x4C,
    BFREQSW    = 0x4D,
    BTREQSW    = 0x4E,
    FREQEW     = 0x4F,
    TREQEW     = 0x50,
    BREQEW     = 0x51,
    BFREQEW    = 0x52,
    BTREQEW    = 0x53,
    DRET       = 0x54,
    POPXJMP    = 0x55, // imm u16
    POPXCJMP   = 0x56, // imm u16
    POPXNCJMP  = 0x57, // imm u16
    CALLPOPA   = 0x58, // imm u16
    POPI0      = 0x59,
    POPI1      = 0x5A,
    POPI2      = 0x5B,
    POPI3      = 0x5C,
    POPF0      = 0x5D,
    POPF1      = 0x5E,
    POPF2      = 0x5F,
    POPF3      = 0x60,
    POPF4      = 0x61,
    POPF5      = 0x62,
    POPF6      = 0x63,
    POPF7      = 0x64,
    POPF8      = 0x65,
    POPF9      = 0x66,
    PUSHI0     = 0x67,
    PUSHI1     = 0x68,
    PUSHI2     = 0x69,
    PUSHI3     = 0x6A,
    PUSHF0     = 0x6B,
    PUSHF1     = 0x6C,
    PUSHF2     = 0x6D,
    PUSHF3     = 0x6E,
    PUSHF4     = 0x6F,
    PUSHF5     = 0x70,
    PUSHF6     = 0x71,
    PUSHF7     = 0x72,
    PUSHF8     = 0x73,
    PUSHF9     = 0x74,
    PUSHAINTER = 0x75, // imm u16
    SYSTEM     = 0x76, // imm u16
    REQWAIT    = 0x77,
    PREQWAIT   = 0x78,
    REQCHG     = 0x79,
    ACTREQ     = 0x7A,
}

public class AtelAssembler {

    private bool _op_has_imm(AtelOp op) {
        return op switch {
            AtelOp.LABEL      or
            AtelOp.TAG        or
            AtelOp.PUSHV      or
            AtelOp.POPV       or
            AtelOp.POPVL      or
            AtelOp.PUSHAR     or
            AtelOp.POPAR      or
            AtelOp.POPARL     or
            AtelOp.PUSHARP    or
            AtelOp.PUSHI      or
            AtelOp.PUSHII     or
            AtelOp.PUSHF      or
            AtelOp.JMP        or
            AtelOp.CJMP       or
            AtelOp.NCJMP      or
            AtelOp.JSR        or
            AtelOp.CALL       or
            AtelOp.PUSHN      or
            AtelOp.PUSHT      or
            AtelOp.PUSHVP     or
            AtelOp.PUSHFIX    or
            AtelOp.POPXJMP    or
            AtelOp.POPXCJMP   or
            AtelOp.POPXNCJMP  or
            AtelOp.CALLPOPA   or
            AtelOp.PUSHAINTER or
            AtelOp.SYSTEM     => true,
            _                 => false,
        };
    }

    private int _disasm(ReadOnlySpan<byte> chunk, int offset) {
        byte op_byte    = chunk[offset];
        bool op_has_imm = op_byte.get_bit (7);
        byte op_code    = op_byte.get_bits(0, 7);

        ArgumentOutOfRangeException.ThrowIfGreaterThan(op_code, 0x7A);

        int    op_size  = op_has_imm ? 3 : 1;
        AtelOp op       = Unsafe.As<byte, AtelOp>(ref op_code);

        if (op_has_imm) {
            byte imm_ptr = chunk[offset + 1];

            if (op is AtelOp.PUSHII) {
                ATEL_IMM_PUSHII op_imm = Unsafe.As<byte, ATEL_IMM_PUSHII>(ref imm_ptr);
                Console.WriteLine($"{offset:X4} {op}({op_imm})");
            }
            else {
                ATEL_IMM op_imm = Unsafe.As<byte, ATEL_IMM>(ref imm_ptr);
                Console.WriteLine($"{offset:X4} {op}({op_imm})");
            }
        }
        else {
            Console.WriteLine($"{offset:X4} {op}");
        }

        return offset + op_size;
    }

    public void disassemble(Span<byte> chunk, string name) {
        Console.WriteLine($"--- {name} ---");

        for (int offset = 0; offset < chunk.Length;) {
            offset = _disasm(chunk, offset);
        }
    }
}
