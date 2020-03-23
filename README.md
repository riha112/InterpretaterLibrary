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
print_line

# ------- New For loop, If statment & auto initializer ------
@for(x @= 0| x < 9| x+=1)
	@for(y @= 0| y < 9| y+=1)
		m @= x * y
		@if(m < 10)
			print: 0
		close
		print: m: " | "
	close
	print_line
close

#---------------Each loop-------------
print_line: "Each loop:"
set testArr as array: [1, 2, 3, 10, 15]
@each(mem|testArr)
	print_line: mem
close

# ----------Function------------
@func PrintTextXTimes(text|times)
	@for(x@=0|x<times|x+=1)
		print_line:text
	close
@endf
PrintTextXTimes("hello - frome function"|4)
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
070 |  01 | 02 | 03 | 04 | 05 | 06 | 07 | 08 | 09 |
071 |  02 | 04 | 06 | 08 | 10 | 12 | 14 | 16 | 18 |
072 |  03 | 06 | 09 | 12 | 15 | 18 | 21 | 24 | 27 |
073 |  04 | 08 | 12 | 16 | 20 | 24 | 28 | 32 | 36 |
074 |  05 | 10 | 15 | 20 | 25 | 30 | 35 | 40 | 45 |
075 |  06 | 12 | 18 | 24 | 30 | 36 | 42 | 48 | 54 |
076 |  07 | 14 | 21 | 28 | 35 | 42 | 49 | 56 | 63 |
077 |  08 | 16 | 24 | 32 | 40 | 48 | 56 | 64 | 72 |
078 |  09 | 18 | 27 | 36 | 45 | 54 | 63 | 72 | 81 |
079 |
080 |  1
081 |  2
082 |  3
083 |  10
084 |  15
085 |  hello - frome function
086 |  hello - frome function
087 |  hello - frome function
088 |  hello - frome function
```
## RAW code (after translation)
```
print_line: Hello world
print_line
set var_num as number: 5
print_line: ___var___xzGlHpn6YEQQ0: var_num
print_line
set ___var___PA7XIXe7Msdk22 as auto: set b as number: 9
print_line: ___var___PA7XIXe7Msdk22
print_line
print_line: ___var___6GxwIphYM1rf1
print_line: ___var___ihxAP5d5lgs92: x
print_line
set comp_a as text: 9
set comp_b as number: 9
set ___var___ngA1bxILAkUg23 as auto: compare.equal: comp_a: comp_b
print_line: ___var___096Tus8Luxx13:        ___var___ngA1bxILAkUg23
set ___var___hi2UJmsM0OrF24 as auto: compare.equal_type: comp_a: comp_b
print_line: ___var___u10H5RUGiBdA4:  ___var___hi2UJmsM0OrF24
set ___var___Ib2FpxbPDUyh25 as auto: compare.equal_value: comp_a: comp_b
print_line: ___var___3Xr2BVFZoxGI5: ___var___Ib2FpxbPDUyh25
print_line:
set arr as array: [5,abcd,[22,43,[vvvv,bbb],true]]
print_line: ___var___tyFu2PVVUFgX6: arr
set ___var___GiajfVEFQ0Qz26 as auto: array.length: arr
print_line: ___var___fG9ExV1z6HZH7: ___var___GiajfVEFQ0Qz26
print_line
set ___var___5vzA9BvKZ4t927 as auto: array.get: arr: 2
print_line: ___var___afkI2IN6wtnH8: ___var___5vzA9BvKZ4t927
set ___var___EdwvifOyVUoB28 as auto: array.length: array.get: arr: 2
print_line: ___var___mANEPsQzQDX49: ___var___EdwvifOyVUoB28
print_line
print_line: ___var___VbCDGgLWJ8tW10
set x as number: 1
open while:compare.smaller:x:10
set y as number: 1
open while:compare.smaller:y:10
set m as number:arithmetic.multiply:x:y
open check:compare.smaller:m:10
print: 0
close
print: m: ___var___Em3l1jBWtDs711
add:y:1
close
print_line
add:x:1
close
print_line
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
print_line: Factorial
set a as number: 0
set b as number: 1
set l0 as number: 0
open while:compare.smaller:l0:10
print: l0: ___var___iyTP0B0HtrX112
set l1 as number: 2
set m as number: 1
open while:compare.smaller:l1:arithmetic.sum:l0:1
set m as number:arithmetic.multiply:m:l1
add:l1:1
close
print_line: m
add:l0:1
close
print_line
set ref as reference: array.get: arr: 2
print_line: ref
print_line
print_line: Sorting
set unsorted_array as array: [4,1,66,43,2,61,-10]
print_line: ___var___fkBpRC8og09D13: unsorted_array
set l0 as number: 0
open while:compare.smaller:l0:array.length: unsorted_array
set l1 as number:arithmetic.sum:l0:1
set max_id as number: l0
open while:compare.smaller:l1:array.length: unsorted_array
set o0 as reference: array.get: unsorted_array: l0
set o1 as reference: array.get: unsorted_array: l1
open check:compare.larger:o0:o1
set tmp as number: o0
update:o0:o1
update:o1:tmp
close
add:l1:1
close
add:l0:1
close
print_line: ___var___yEHGvaMtUFvi14: unsorted_array
print_line
set x as auto:0
open while:compare.smaller:x:9
add:x:1
set y as auto:0
open while:compare.smaller:y:9
add:y:1
set m as auto:arithmetic.multiply:x:y
set ___var___liGejWZSwDK529 as auto:compare.smaller:m:10
open check: ___var___liGejWZSwDK529
print: 0
close
set ___var___8MPbAYtLZbPv30 as auto:arithmetic.multiply:x:y
print: ___var___8MPbAYtLZbPv30: ___var___x65hhWEnAvgQ15
close
print_line
close
print_line
print_line: Sorting with for loop
set unsorted_array_2 as array: [9.5, 12.2, -12, 22, 1000, 42]
print_line: ___var___Tlf1Ev1syrrx16: unsorted_array_2
set l0 as auto:0
open while:compare.smaller:l0:array.length: unsorted_array_2
set l1 as auto:arithmetic.sum:l0:1
open while:compare.smaller:l1:array.length: unsorted_array_2
set o0 as reference: array.get: unsorted_array_2: l0
set o1 as reference: array.get: unsorted_array_2: l1
set ___var___ACwLmtOkg7n131 as auto:compare.larger:o0:o1
open check: ___var___ACwLmtOkg7n131
set tmp as number: o0
update:o0:o1
update:o1:tmp
close
add:l1:1
close
add:l0:1
close
print_line: ___var___3uFsPEhL0t3U17: unsorted_array_2
print_line
set ___var___AoGdZAYayYgk34 as auto:arithmetic.sum:5:2
set ___var___vQTDxdyCKSN036 as auto:arithmetic.multiply:4:4
set ___var___yoGoyOK9yZkJ35 as auto:arithmetic.sum:4:___var___vQTDxdyCKSN036
set ___var___AN77CNLkDEPS33 as auto:arithmetic.multiply:___var___AoGdZAYayYgk34:arithmetic.sum:3:___var___yoGoyOK9yZkJ35
set ___var___hxXY3QuLe6Si38 as auto:arithmetic.sum:5:2
set ___var___CHywRIdIgGwa40 as auto:arithmetic.multiply:4:4
set ___var___RwOvAPLQMPjY39 as auto:arithmetic.sum:4:___var___CHywRIdIgGwa40
set ___var___eJldMviMGWKm37 as auto:arithmetic.multiply:___var___hxXY3QuLe6Si38:arithmetic.sum:3:___var___RwOvAPLQMPjY39+1
set ___var___5pAdfLNbDBdG32 as auto:compare.smaller:___var___AN77CNLkDEPS33:___var___eJldMviMGWKm37
open check: ___var___5pAdfLNbDBdG32
print: ___var___G0gvxWvwx1X018
close
set mm as auto:0
add:mm:arithmetic.sum:5:arithmetic.multiply:10:3
print_line: mm
print_line: ___var___5UYRNvUfQJoY19
set testArr as array: [1, 2, 3, 10, 15]
set ___var___6H4Ln59vGOw421 as number: -1
set ___var___rJI3zvWi1dxp41 as auto: array.length:testArr
open while:compare.smaller:___var___6H4Ln59vGOw421:arithmetic.sum:___var___rJI3zvWi1dxp41:-1
add:___var___6H4Ln59vGOw421:1
set mem as reference: array.get: testArr: ___var___6H4Ln59vGOw421
print_line: mem
close
set text as reference: ___var___rmI65GZBtvvO20
set times as reference: 4
goto:PrintTextXTimes
return
label:PrintTextXTimes
set x as auto:0
open while:compare.smaller:x:times
add:x:1
print_line:text
close
return
```
