# Zrukall
A text adventure game engine made with Unity 5.

# Zrukall Script Language

## Identifiers:

Just use the same pattern you would use in a c-style language. Letters, digits and underscore is valid, but don't start the identifier with a number. If you are spanish, you can also use the Ñ and the vowels with an accent.

## Types:

You can use 2 types of entities: rooms & objects. If you want to declare a room use:

```
room my_room:
    # This a one line comment.
    # Events declarations...
end
```

There is a special room with the name `main`, this one is where the game will start, so you can use it to initialize the game and then go to the first real room of your game. To declare an object use:

```
object my_object:
    # Events declarations...
end
```

## Events:

You can use 3 types of events:

```
on enter:
    # This will be called when you enter a room.
    # Statements...
end
```

```
on exit:
    # This will be called when you exit a room.
    # Statements...
end
```

```
on "action":
    # This will be called when the input matches.
    # Statements...
end
```

The actions can be any string you want, but don't use any symbol. Use only one space to separate the words, and you also can use the symbol pipe (\|) if you want to use different patterns to execute one event. Examples:

* "look"
* "use button"
* "drop rock\|throw rock"

## Statements:

You can use the following statements in the code of the events: calling a function, let assignment, if, while, for. In the next section you will be able to read about the BIFs (Built-In Functions) of this language. To call a function you have to:

```
writeln "Hello, world!",
writeln "2 + 3 = " (2 + 3)
```

If you want to have more than 1 statement in an event, you will have to divide them with commas. The function parameters are divided by spaces, so you will have to use parenthesis if you want to put an expression instead of a value. To assign values to variables you will need the let statement:

```
let counter = 0,
let counter = counter + 1,
writeln counter
# Output: 1
```

The scope of the variables in the language are global, so be careful. To do conditional statements, you can use ifs like this:

```
if number % 2 = 0:
    writeln number " is even"
end
```

```
if number % 2 <> 0:
    writeln number " is odd"
else:
    writeln number " is even"
end
```

```
if number = 0:
    writeln number " is zero"
else if number < 0:
    writeln number " is negative"
else:
    writeln number " is positive"
end
```

You can also use loops if you want with while and for. The for statement also has some peculiarities, you can use the keyword `to` or the operator `..` to make an ascending count; with `downto` the count will be a descending one.

```
let i = 1,
while i <= 10:
    writeln (i ^ 2),
    let i = i + 1
end
```

```
for i in 1 .. 10:
    writeln (i ^ 2)
end
```

```
for i in 1 to 10:
    writeln (i ^ 2)
end
```

```
for i in 10 downto 1:
    writeln (i ^ 2)
end
```

## Types of values:

In this language there are 4 types of values:

* Strings: "My name is Iñigo Montoya. You kill my father. Prepare to die!"
* Floats: 3.14, 0.123e-45
* Integers: 1234
* Booleans: true, false

## Operators:

### Arithmetic

* Unary: +, -
* Binary: +, -, *, /, %, div, mod

The operator `%` and `mod` are the same, but `div` makes an integer division while `/` can be a float division if one of the values is a float number. The other operators are quite the same as any c-syntax language, except the `+` operator if you use a string value in any side, because this will convert any other value to a string and concatenate both string values.

You can use a string in any kind of arithmetic operation, and it will try to convert the string to float or integer. If the conversion can't be done, it will return zero as value. So be careful if you use strings.

### Logical

* Unary: not
* Binary: and, xor, or, <, >, <=, >=, =, <>

The only time the equals operator is not use to compare is in the let statement (only the first equal in the line). The `<>` represents the not equals operator. The other operators are quite similar to other languages.

### Precedence

1. not, +, - (unary)
2. ^
3. *, /, %, div, mod
4. +, -
5. <, >, <=, >=
6. =, <>
7. and
8. xor
9. or

## Functions:

### Configuration:

* `setdefmsg message`: Sets the default message if the input does not match with any action event in the current room.
* `setrunfst`: Sets the action match process to trigger only the first matched action event.
* `setrunall`: Sets the action match process to trigger all the matched action event.

### Output:

* `writeln parameters`: Writes a line in the console with an end of line after the message. It's the same that `write parameters "\n"`.
* `write parameters`: Writes a line in the console.

### Rooms:

* `goto room_name`: Sets the next room to change after the event is executed.
* `roomname`: Gets the name of the current room.
* `lastroom`: Gets the last room before the current one.

### Inventory:

* `invadd obj_name`: Adds an object in the inventory.
* `invrem obj_name`: Removes an object in the inventory.
* `invhas obj_name`: Checks if an object exists in the inventory.

### Special:

* `finish`: Finish the game. The user will start a new one after pressing any key.
* `reset`: Resets the game. It's similar to `finish` but the player won't need to press any key to start the new game.
