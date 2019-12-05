/* Structure of an element of a linked list */
typedef struct s_list {
	int value;
	struct s_list* next;
} list_elem_t;

/* Prototypes */
int insert_head(list_elem_t** l, int value);
int insert_tail(list_elem_t** l, int value);
int remove_element(list_elem_t** ppl, int value);
list_elem_t* get_tail(list_elem_t* l);
void reverse_list(list_elem_t** l);
list_elem_t* find_element(list_elem_t* l, int index);
int list_size(list_elem_t* l);

