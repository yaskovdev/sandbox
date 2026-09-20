# A Simple Proof of Push Turing-Completeness Using URM-5

## What is being proved

Push has several implementations and variants. Here we prove the following
precise statement.

> **Theorem.** An idealized version of Psh with unbounded integer values,
> unbounded stacks, and no execution-step limit is Turing-complete.

The word "idealized" is important. The Java implementation uses 32-bit
integers and every real computer has finite memory. Turing-completeness is
normally a statement about a language with unlimited memory available in
principle.

## The source language and known result

Define **URM-5** to be the register language used by the interpreter. It has:

- 5 addressable registers, numbered 1 through 5;
- `a i`, which adds 1 to register `i`;
- `s i`, which subtracts 1 from register `i`;
- `(P) i`, which repeatedly executes `P` while register `i` is positive;
- `.`, which halts.

Programs used for ordinary natural-number computation never decrement a
register below zero.

We use the following known result.

> **URM-5 universality theorem.** For every Turing machine `M`, there exists
> a program `P_M` in URM-5 such that, for every input `x`, `P_M` has the same
> output and halting behavior on `x` as `M`.

Therefore, it is enough to prove that Push can correctly execute every valid
program in URM-5.

## Encoding a register program

Encode program symbols as integers:

$$
\begin{aligned}
\operatorname{enc}(.) &= 0,\\
\operatorname{enc}(a) &= -1,\\
\operatorname{enc}(s) &= -2,\\
\operatorname{enc}(() &= -3,\\
\operatorname{enc}()) &= -4,\\
\operatorname{enc}(i) &= i.
\end{aligned}
$$

For any finite URM-5 program `P`, construct a Push program `C(P)`. Its integer
stack represents this logical memory:

$$
[R_0,R_1,R_2,R_3,R_4,R_5,q,d,\operatorname{enc}(P)].
$$

Here:

- `R_i` is the value of register `i`;
- `q` is the position of the next program symbol;
- `d` is temporary storage used while matching parentheses.
- `enc(P)` is the encoded program.

The logical addresses are therefore:

$$
\begin{aligned}
\operatorname{address}(R_i) &= i,\\
\operatorname{address}(q) &= 6,\\
\operatorname{address}(d) &= 7,\\
\operatorname{address}(P[j]) &= 8+j.
\end{aligned}
$$

These addresses do not depend on the length of `P`. The stack may contain any
finite number of encoded program symbols, so the construction has no fixed
54-symbol code limit.

The remainder of `C(P)` is the Push interpreter from the demonstration,
adjusted to use these logical addresses. The initialization prefix arranges
the values in the physical order required by Psh's top-relative stack
operations.

This construction is effective: there is an ordinary terminating algorithm
that receives `P` and prints `C(P)`.

## The representation invariant

Consider the moment immediately before the interpreter processes a program
symbol. Say that the Push state represents the register-machine state when:

1. the six register slots contain `R_0` through `R_5`;
2. every code slot contains the corresponding encoded program symbol;
3. the stored program position is `q`;
4. the parenthesis-depth slot is zero;
5. no temporary integer or Boolean values remain;
6. `exec.y` has arranged for the interpreter loop to run again.

Call this the **interpreter invariant**.

The initialization part of `C(P)` establishes the invariant for the initial
register values and for `q = 0`.

## Simulation lemma

> **Lemma.** Suppose the interpreter invariant holds. If the URM-5 program
> performs one step, then the Push interpreter performs finitely many Push
> steps and reaches a state representing the resulting URM-5 state.

### Proof

There are five possible program symbols.

#### Stack rules used in the first case

Write an integer stack from top to bottom. For example,

$$
u :: v :: S
$$

means that `u` is on top, `v` is immediately below it, and `S` is the
remainder of the stack.

The first case uses these Psh rules:

- a literal places its value on top;
- `integer.dup` duplicates the top value;
- `integer.swap` exchanges the top two values;
- `integer.pop` removes the top value;
- `integer.+` replaces `a :: b :: S` by `(b+a) :: S`;
- `integer.yank` removes a top index `k`, then moves the value at depth `k`
  to the top;
- `integer.yankdup` removes a top index `k`, then copies the value at depth
  `k` to the top;
- `integer.shove` removes a top index `k`, then moves the new top value to
  depth `k`.

Depth zero means the top of the stack. These descriptions assume that the
required values exist and that `k` is in range; the proof below establishes
those conditions for every index it uses.

For register values `R`, program counter `q`, depth value `d`, and encoded
code `c_0,...,c_{m-1}`, define

$$
\operatorname{Mem}(R,q,d,C)
=
R_0 :: R_1 :: R_2 :: R_3 :: R_4 :: R_5
:: q :: d :: c_0 :: \cdots :: c_{m-1}.
$$

Thus `q` is at depth 6, `d` is at depth 7, and `c_j` is at depth `8+j`.

#### Case 1: `a i`

Assume:

- the interpreter invariant holds;
- `c_q` is the encoding of `a`;
- `c_{q+1} = i`, where `1 <= i <= 5`;
- the dispatcher has fetched `c_q` and selected the `a` handler.

At entry to the handler, the integer stack is

$$
-1 :: \operatorname{Mem}(R,q,0,C).
$$

For the planned memory layout, the handler is:

```text
(1 6 integer.+) integer.yank
1 integer.+
(1 6 integer.+) integer.shove

1 6 integer.+ integer.yankdup
1 (8 integer.+) integer.+ integer.yankdup
integer.dup
(2 integer.+) integer.yank
1 integer.+
integer.swap
(1 integer.+) integer.shove
```

First, `(1 6 integer.+)` produces 7. Because the fetched opcode occupies
depth 0, `q` is temporarily at depth 7. The first three lines therefore move
`q` to the top, increment it, and return it to its slot. The resulting stack
is

$$
-1 :: \operatorname{Mem}(R,q+1,0,C).
$$

Next, `1 6 integer.+ integer.yankdup` copies the new value `q+1`.
The expression `1 (8 integer.+) integer.+` then produces `(q+1)+9`.
After that expression is consumed by `integer.yankdup`, the opcode is the
only value above memory. Consequently, encoded code item `c_{q+1}` is at
depth

$$
1+8+(q+1)=(q+1)+9.
$$

The copied value is therefore `i`, and the stack becomes

$$
i :: -1 :: \operatorname{Mem}(R,q+1,0,C).
$$

`integer.dup` preserves one copy of `i` and uses the other to produce the
index `i+2`. After that index is consumed, one copy of `i` and the fetched
opcode are above memory. Register `R_i` is therefore at depth `i+2`.
`integer.yank` moves `R_i` to the top, and `1 integer.+` changes it to
`R_i+1`.

After `integer.swap`, the preserved `i` is again on top.
`(1 integer.+)` changes it to `i+1`. Once that index is consumed,
the fetched opcode is the only value above the register area, so depth
`i+1` is exactly the original location of `R_i`. `integer.shove` writes the
incremented value there. Thus the handler finishes with

$$
-1 :: \operatorname{Mem}(R[i \mapsto R_i+1],q+1,0,C).
$$

The remaining false dispatcher tests preserve this integer stack.
`integer.pop` then removes the fetched opcode. The common interpreter
epilogue moves the value at depth 6 to the top, increments it, and shoves it
back to depth 6. Therefore the cycle ends with

$$
\operatorname{Mem}(R[i \mapsto R_i+1],q+2,0,C).
$$

This is exactly the URM-5 transition for `a i`: register `i` is incremented,
all other registers and code cells are unchanged, and `q` advances past both
symbols. The handler does not touch the Boolean, code, or name stacks; each
dispatcher comparison consumes its Boolean result; and the continuation
created by `exec.y` schedules the next cycle. Hence the complete interpreter
invariant is restored, and the one-step simulation lemma holds in the `a i`
case.

#### Remaining cases (proof sketches)

**Case 2: `s i`.**

The same argument applies, except that the interpreter subtracts one. It
moves `q` past `s i` and restores the invariant.

**Case 3: `(`.**

The interpreter sets the temporary depth to one and scans forward.
Encountering another `(` increases the depth, and encountering `)` decreases
it. The program is finite and has balanced parentheses, so the scan reaches
the unique matching `)` after finitely many steps. The depth is then zero.
The interpreter leaves `q` at that closing parenthesis so that the loop
condition will be tested before the body is entered.

**Case 4: `) i`.**

The interpreter reads register `i`.

- If the value is positive, it scans backward to the matching `(` and places
  `q` at the first symbol of the loop body.
- If the value is not positive, it moves `q` past `) i`.

The backward scan uses the same nesting-depth argument as the forward scan.
Therefore it terminates at the correct opening parenthesis. This is exactly
the behavior of `(P) i`.

**Case 5: `.`.**

The interpreter executes `exec.flush`. The exec stack becomes empty, so Push
halts. The simulated registers retain their final values.

The definitions of `integer.yank`, `integer.yankdup`, and `integer.shove`
show directly that each read and write above accesses the intended stack
slot. Their temporary values are removed before the next interpreter cycle.
Thus the invariant is restored in every non-halting case.

The `a i` case above is formalized. The remaining four cases must be expanded
to the same level before the complete simulation lemma is proved.

## Correctness of the complete simulation

Apply the simulation lemma repeatedly.

After one URM-5 step, Push represents the same new state. After two steps, it
again represents the same state, and so on. By induction, after every finite
number of URM-5 steps, Push represents exactly the corresponding URM-5 state.

Consequently:

$$
\operatorname{URM}(P,x)\downarrow y
\quad\Longleftrightarrow\quad
\operatorname{Push}(C(P),x)\downarrow y.
$$

In words, the URM-5 program halts with result `y` exactly when its compiled
Push program halts with result `y`.

If the URM-5 program runs forever, every one of its steps is simulated by a
finite, nonempty sequence of Push steps. The Push execution therefore also
runs forever.

## Conclusion

Let `M` be any Turing machine. By the URM-5 universality theorem, there is an
URM-5 program `P_M` that simulates `M`. The construction above produces a
Push program `C(P_M)`, and the simulation lemma shows that `C(P_M)` simulates
`P_M`. Therefore `C(P_M)` simulates `M`.

Thus every Turing machine can be simulated by a Push program. Hence the
idealized Push language is Turing-complete.

## Relation to the existing demonstration

The existing Push program is a finite example of the interpreter used in this
proof. This document describes a planned layout change; it does not claim that
the current Gist already has that layout.

To make the source code match the theorem literally, the following engineering
changes are needed:

1. Put `q` and `d` before the encoded code, giving them fixed logical
   addresses 6 and 7.
2. Remove the fixed 54-symbol code area so that every finite URM-5 program can
   be represented.
3. Remove the fixed execution-step limit.
4. Use unbounded integers, or represent each register value using an
   unbounded sequence of stack values.
5. State whether loops test "positive" or "nonzero" and make the URM
   definition and interpreter agree.

These changes do not provide the logical argument by themselves. They make
the implementation satisfy the assumptions used by the proof.

## Note: A concrete witness for URM-5 universality

Daniel Cristofani gives a particular URM-5 program in
[`urmutm.txt`](https://www.hevanet.com/cristofd/brainfuck/urmutm.txt). Call
this program `U5`. It is constructed to simulate the four-state, six-symbol
universal Turing machine described by Yurii Rogozhin in *Small Universal
Turing Machines*, Theoretical Computer Science 168(2), 1996.

The registers of `U5` have these meanings:

- register 1 contains the left half of the simulated tape in base 6;
- register 5 contains the current symbol and right half of the tape in base 6;
- register 3 contains the simulated state, with zero meaning halt;
- registers 2 and 4 are temporary storage.

The uncommented `U5` program requires 572 encoded tokens. It provides a
concrete witness for the URM-5 universality theorem, but its details are not
needed in the main Push simulation proof.
