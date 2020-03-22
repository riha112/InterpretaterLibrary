# Interpreter Library
Rewriting old interpreter for custom language

## Demo code:
```
# ----------Hello world program using unsafe-string----------
print_line: Hello world
print_line

# ----------Setting variable & printing it----------
set var_num as number: 5
print_line: "Variables 'var_num' value is :": var_num
print_line

# ----------Chain actions with priority brackets----------
print_line: (set b as number: 9)
print_line

# ----------Safe string stored in quotes----------
print_line: "set x as text: print_line: compare.equal: a: b"
print_line: "Previous action didn't execute because it was stored in safe string, thus x=": x
print_line

# ----------Comparisons----------
set comp_a as text: 9
set comp_b as number: 9
print_line: "A === B: ":        (compare.equal: comp_a: comp_b)
print_line: "A TYPE SAME B: ":  (compare.equal_type: comp_a: comp_b)
print_line: "A VALUE SAME B: ": (compare.equal_value: comp_a: comp_b)
print_line:

# ----------Simple array----------
set arr as array: [5,abcd,[22,43,[vvvv,bbb],true]]
print_line: "Array: ": arr
print_line: "Array length is: ": (array.length: arr)
print_line

# ----------Sub-array----------
print_line: "Sub-array of array is: ": (array.get: arr: 2)
print_line: "Sub-array length is: ": (array.length: array.get: arr: 2)
print_line


# ----------Multiplication table----------
print_line: "Multiplication table:"
set x as number: 1
open while: x < 10
	set y as number: 1
	open while: y < 10
		set m as number: x * y
		open check: m < 10
			print: 0
		close
		print: m: " | "
		y += 1
	close
	print_line
	x += 1
close
print_line

# ----------Fibonacci sequence----------
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

# ----------Factorial----------
print_line: Factorial
set a as number: 0
set b as number: 1

set l0 as number: 0
open while: l0 < 10
	print: l0: "!="

	set l1 as number: 2
	set m as number: 1
	open while: l1 < l0 + 1
		
		set m as number: m * l1
		l1 += 1

	close
	print_line: m
	l0 += 1
close
print_line

# ----------Reference variable----------
set ref as reference: array.get: arr: 2
print_line: ref
print_line

# ----------Sorting----------
print_line: Sorting
set unsorted_array as array: [4,1,66,43,2,61,-10]
print_line: "Unsorted array: ": unsorted_array

set l0 as number: 0
open while: l0 < array.length: unsorted_array
	set l1 as number: l0 + 1
	set max_id as number: l0
	open while: l1 < array.length: unsorted_array
		set o0 as reference: array.get: unsorted_array: l0
		set o1 as reference: array.get: unsorted_array: l1
		open check: o0 > o1
			set tmp as number: o0
			o0 = o1
			o1 = tmp
		close
		l1 += 1
	close
	l0 += 1
close

print_line: "Sorted array: ": unsorted_array
```
---
## Output
```
000 |  Hello world
001 |
002 |  Variables 'var_num' value is :5
003 |
004 |  9
005 |
006 |  set x as text: print_line: compare.equal: a: b
007 |  Previous action didn't execute because it was stored in safe string, thus x=x
008 |
009 |  A === B: False
010 |  A TYPE SAME B: False
011 |  A VALUE SAME B: True
012 |
013 |  Array: [5,abcd,[22,43,[vvvv,bbb],True]]
014 |  Array length is: 3
015 |
016 |  Sub-array of array is: [22,43,[vvvv,bbb],True]
017 |  Sub-array length is: 4
018 |
019 |  Multiplication table:
020 |  01 | 02 | 03 | 04 | 05 | 06 | 07 | 08 | 09 |
021 |  02 | 04 | 06 | 08 | 10 | 12 | 14 | 16 | 18 |
022 |  03 | 06 | 09 | 12 | 15 | 18 | 21 | 24 | 27 |
023 |  04 | 08 | 12 | 16 | 20 | 24 | 28 | 32 | 36 |
024 |  05 | 10 | 15 | 20 | 25 | 30 | 35 | 40 | 45 |
025 |  06 | 12 | 18 | 24 | 30 | 36 | 42 | 48 | 54 |
026 |  07 | 14 | 21 | 28 | 35 | 42 | 49 | 56 | 63 |
027 |  08 | 16 | 24 | 32 | 40 | 48 | 56 | 64 | 72 |
028 |  09 | 18 | 27 | 36 | 45 | 54 | 63 | 72 | 81 |
029 |
030 |  Fibonacci sequence
031 |  1
032 |  2
033 |  3
034 |  5
035 |  8
036 |  13
037 |  21
038 |  34
039 |  55
040 |  89
041 |  144
042 |  233
043 |  377
044 |  610
045 |  987
046 |  1597
047 |  2584
048 |  4181
049 |  6765
050 |  10946
051 |
052 |  Factorial
053 |  0!=1
054 |  1!=1
055 |  2!=2
056 |  3!=6
057 |  4!=24
058 |  5!=120
059 |  6!=720
060 |  7!=5040
061 |  8!=40320
062 |  9!=362880
063 |
064 |  [22,43,[vvvv,bbb],True]
065 |
066 |  Sorting
067 |  Unsorted array: [4,1,66,43,2,61,-10]
068 |  Sorted array: [-10,1,2,4,43,61,66]
069 |
```
