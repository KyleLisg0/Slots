Feature: Slots Machine

Simple slot machine game

Scenario: Run the machine with the specified config
	Given the reel width is 3
	And the line count is 4
	And the available symbols are:
	| Character | Name           | Coefficient | Probability | IsWildCard |
	| A         | Apple          | 0.4         | 0.45        | 0          |
	| B         | Banana         | 0.6         | 0.35        | 0          |
	| P         | Pinaple        | 0.8         | 0.15        | 0          |
	| *         | SimpleWildCard | 0           | 0.05        | 1          |
	
	And the slot machine is running
	And the player wallet has <WalletStart>
	And the input stake is <stake>
	And the reel spin is <spin>

	When we pull the handle
	Then we should get a result out
	And we show the wallet
	And the winnings displayed are <Total Winnings>
	And the final player wallet is <WalletEnd>

	Examples: 
	| WalletStart | stake | spin              | Total Winnings | WalletEnd |
	| 200         | 10    | 'AAA,BBB,PPP,***' | 54.0           | 244.0     |
	| 200         | 10    | 'AAB,BBP,PPA,AB*' | 0              | 190.0     |
	| 200         | 10    | 'AAA,BBP,PPA,AB*' | 12             | 202.0     |
	| 200         | 10    | 'BAA,AAA,A*B,*AA' | 20             | 210.0     |
	| 200         | 100   | '*P*,AAA,BBB,PPP' | 620            | 720.0     |
	| 200         | 100   | '***,***,***,***' | 0              | 100.0     |

Scenario: Invalid input
	Given the reel width is 3
	And the line count is 4
	And the available symbols are:
	| Character | Name           | Coefficient | Probability | IsWildCard |
	| A         | Apple          | 0.4         | 0.45        | 0          |
	| B         | Banana         | 0.6         | 0.35        | 0          |
	| P         | Pinaple        | 0.8         | 0.15        | 0          |
	| *         | SimpleWildCard | 0           | 0.05        | 1          |
	
	And the slot machine is running
	And the player wallet has <WalletStart>
	And the input stake is <stake>

	When we pull the handle
	Then an error is shown
	But we didn't take any money, so their wallet is still <WalletStart>

	Examples: 
	| WalletStart | stake |
	| 200         | -1    |
	| 100         | 500   |
	| 100         | 0     |
	| 0           | 50    |