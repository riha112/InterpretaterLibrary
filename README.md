# Interpreter Library
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
print_line:

set arr as array: [5,abcd,[22,43,[vvvv,bbb],true]]

print_line: Array length is 
print: array.length: arr
print_line: arr
print_line


print_line: Sub-array lenght is 
print: array.length: array.get: arr: 2
print_line: array.get: arr: 2
print_line
print_line

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




set ref as reference: array.get: arr: 2
print_line: ref


# Sorting
set unsorted_array as array: [4,1,66,43,2,61,-10]
print_line: Unsorted array = 
print: unsorted_array
print_line

set l0 as number: 0
open while: compare.smaller: l0: array.length: unsorted_array
	set l1 as number: arithmetic.sum: l0: 1
	set max_id as number: l0
	open while: compare.smaller: l1: array.length: unsorted_array
		set o0 as reference: array.get: unsorted_array: l0
		set o1 as reference: array.get: unsorted_array: l1
		open check: compare.larger: o0: o1
			set tmp as number: o0
			update: o0: o1
			update: o1: tmp
		close
		add: l1: 1
	close
	add: l0: 1
close

print_line
print_line: Sorted array = 
print: unsorted_array
print_line
```
---
## Output
```
 00 |  5
 01 |  9
 02 |  Hello world
 03 |  False
 04 |  False
 05 |  False
 06 |  False
 07 |  True
 08 |  False
 09 |
 10 |  Array length is 3
 11 |  [5,abcd,[22,43,[vvvv,bbb],True]]
 12 |
 13 |  Sub-array lenght is 4
 14 |  [22,43,[vvvv,bbb],True]
 15 |
 16 |  01|02|03|04|05|06|07|08|09|
 17 |  02|04|06|08|10|12|14|16|18|
 18 |  03|06|09|12|15|18|21|24|27|
 19 |  04|08|12|16|20|24|28|32|36|
 20 |  05|10|15|20|25|30|35|40|45|
 21 |  06|12|18|24|30|36|42|48|54|
 22 |  07|14|21|28|35|42|49|56|63|
 23 |  08|16|24|32|40|48|56|64|72|
 24 |  09|18|27|36|45|54|63|72|81|
 25 |
 26 |  Fibonacci sequence
 27 |  1
 28 |  2
 29 |  3
 30 |  5
 31 |  8
 32 |  13
 33 |  21
 34 |  34
 35 |  55
 36 |  89
 37 |  144
 38 |  233
 39 |  377
 40 |  610
 41 |  987
 42 |  1597
 43 |  2584
 44 |  4181
 45 |  6765
 46 |  10946
 47 |
 48 |  Factorial
 49 |  0!=1
 50 |  1!=1
 51 |  2!=2
 52 |  3!=6
 53 |  4!=24
 54 |  5!=120
 55 |  6!=720
 56 |  7!=5040
 57 |  8!=40320
 58 |  9!=362880
 59 |  [22,43,[vvvv,bbb],True]
 60 |  Unsorted array = [4,1,66,43,2,61,-10]
 61 |
 62 |
 63 |  Sorted array = [-10,1,2,4,43,61,66]
 64 |
```
