# InterpretaterLibrary
Rewriting old interpreter for custom language

## Demo code:
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


## Fibonacci sequence:

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

## Factorial:

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
