#include <malloc.h>
#include <stdio.h>

struct Node {

	int value;
	Node* next;

};

class List {

private: 
	Node* root;

public:

	List() {
		this->root = nullptr;
	}

	int insert_at_end(int newVal) {

		Node* newNode = (Node*)malloc(sizeof(Node));
		newNode->value = newVal;
		newNode->next = nullptr;

		if (this->root == nullptr) {
			this->root = newNode;
			return 1;
		}
		else {
			Node* current = root;
			while (current->next != nullptr) {
				current = current->next;
			}
			current->next = newNode;
			return 1;
		}
	}

	int print_all() {

		if (this->root == nullptr) {
			return 1;
		}

		Node* current = this->root;
		printf("%d\n", current->value);
		while (current->next != nullptr) {
			current = current->next;
			printf("%d\n", current->value);
		}
		return 1;

	}

};

int main() {

	List newList = List();


	for (int i = 0; i < 10; i++) {
		newList.insert_at_end(i);
	}

	newList.print_all();
	return 0;
}