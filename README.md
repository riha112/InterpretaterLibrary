# InterpretaterLibrary
Rewriting old interpreter for custom language

## Demo code:
```
set variable as number: 5
print_line: variable
print_line: set b as number: 9
print_line: Hello world
print_line: set x as text: print_line: compare.equal: a: b
print_line: x

print_line: compare.equal: variable: b
print_line: compare.equal_type: variable: b
print_line: compare.equal_value: variable: b


set x as number: 1
open while: compare.smaller: x: 10
	set y as number: 1
	open while: compare.smaller: y: 10
		set m as number: arithmetic.multiply: x: y

		open check: compare.smaller: m: 10
			print: 0
		close

		print: m
		print: |
		add: y: 1
	close
	print_line
	add: x: 1
close


# Fibonacci sequence:

print_line: Fibonacci sequence
set a as number: 0
set b as number: 1

set l0 as number: 0
open while: compare.smaller: l0: 20
	
	set tmp as number: arithmetic.sum: a: b
	print_line: tmp
	set a as number: b
	set b as number: tmp

	add: l0: 1
close

print_line

# Factorial:

print_line: Factorial
set a as number: 0
set b as number: 1

set l0 as number: 0
open while: compare.smaller: l0: 10
	print_line
	
	print: l0
	print: !
	print: =

	set l1 as number: 2
	set m as number: 1
	open while: compare.smaller: l1: arithmetic.sum: l0: 1
		
		set m as number: arithmetic.multiply: m: l1
		add: l1: 1

	close
	print: m

	add: l0: 1
close
```
---
## Output
```
 01 |  9
 02 |  Hello world
 03 |  False
 04 |  False
 05 |  False
 06 |  False
 07 |  True
 08 |  False01|02|03|04|05|06|07|08|09|
 09 |  02|04|06|08|10|12|14|16|18|
 10 |  03|06|09|12|15|18|21|24|27|
 11 |  04|08|12|16|20|24|28|32|36|
 12 |  05|10|15|20|25|30|35|40|45|
 13 |  06|12|18|24|30|36|42|48|54|
 14 |  07|14|21|28|35|42|49|56|63|
 15 |  08|16|24|32|40|48|56|64|72|
 16 |  09|18|27|36|45|54|63|72|81|
 17 |
 18 |  Fibonacci sequence
 19 |  1
 20 |  2
 21 |  3
 22 |  5
 23 |  8
 24 |  13
 25 |  21
 26 |  34
 27 |  55
 28 |  89
 29 |  144
 30 |  233
 31 |  377
 32 |  610
 33 |  987
 34 |  1597
 35 |  2584
 36 |  4181
 37 |  6765
 38 |  10946
 39 |
 40 |  Factorial
 41 |  0!=1
 42 |  1!=1
 43 |  2!=2
 44 |  3!=6
 45 |  4!=24
 46 |  5!=120
 47 |  6!=720
 48 |  7!=5040
 49 |  8!=40320
 50 |  9!=362880
```
