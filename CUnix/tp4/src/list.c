/**************************************************************************
 * Louis Robert & Marius Lumbroso
 * 			TP linked lists
 *
 * Group 	:
 * Name 1 :
 * Name 2 :
 *
 **************************************************************************/

#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include "list.h"

/*
 * SYNOPSYS    :
 *   list_elem_t * create_element(int value)
 * DESCRIPTION :
 *   creates a new element, whose field next is initialized with NULL and the field
 *   value is initialized with the integer passed as argument.
 * PARAMETERS :
 *   value : value of the element
 * RESULT :
 *   NULL in case of error, otherwise a pointer over one strusture of type list_elem_t
 */
static list_elem_t * create_element (int value)
{
  list_elem_t * newelt = malloc(sizeof (list_elem_t));
 
  if (newelt != NULL) 
  {
    newelt->value = value;
    newelt->next = NULL;
  }
  return newelt;
}

/*
 * SYNOPSYS    :
 *   void free_element(list_elem_t * l)
 * DESCRIPTION : 
 *   frees an element of the list.
 * PARAMETERS  :
 *   l : the pointer od the element to be freed
 * RESULT   :
 *   nothing
 */

static void free_element (list_elem_t* l)
{
	free(l);
}

/*
 * SYNOPSYS :
 *   int insert_head(limallocst_elem_t * * l, int value)
 * DESCRIPTION :malloc
 *   Insert an element mallocat the head of the list
 *   *l points the new mallochead of the list.
 * PARAMETERS :malloc
 *   list_elem_t ** l :malloc pointer to the pointer of the head of the list
 *   int value : value mallocof the element that is added
 * RESULT :
 *    0 in case of successful insertion
 *   -1 if the insertion is impossible
 */
int insert_head (list_elem_t ** l, int value)
{
   if (l == NULL)
   {
	    return -1; 
   }

  list_elem_t * new_elt = create_element (value);
  if (new_elt == NULL) 
  {
	   return -1;
  }

  new_elt->next = *l;
  *l = new_elt;
  return 0;
}

/*
 * SYNOPSYS :
 *   int insert_tail(list_elem_t * * l, int value)
 * DESCRIPTION :
 *   Insert an element at the tail of the list (* l points the head of the list)
 * PARAMETERS :
 *   list_elem_t ** l : pointer to the pointer of the head of the list
 *   int value : value of the element that is added
 * RESULT :
 *    0 in case of successful insertion
 *   -1 if the insertion is impossible
 */
int insert_tail(list_elem_t ** l, int value) 
{
  list_elem_t* d = *l;

	 
  if (d->next != NULL)
  {
     return insert_tail(&d->next,value);
  }
  else
  {
     d->next = create_element(value); 

     if (d->next == NULL)
     {
     	return 0;
     }
     else
     {
		return 1;
	}
  }
}

/*
 * SYNOPSYS :
 *   list_elem_t * find_element(list_elem_t * l, int index)
 * DESCRIPTION :
 *   Return a pointer of the element at the position n°i of the list
 *   (The first element is situated at the position 0).
 * PARAMETERS :
 *   int index : position of the element to be found
 *   list_elem_t * l : pointer of the head of the list
 * RESULTAT :
 *   - a pointer to the element of the list in cas of success
 *   - NULL in case of error
 */
list_elem_t * find_element(list_elem_t * l, int index) 
{
 	if(l != NULL)
	{
		if(index == 0)
		{
			return l;
		}
          else
		{
			return find_element(l->next,index-1);
		}
	}
	else
	{
		return NULL;
	}

}


list_elem_t* find_element_by_value(list_elem_t * l, int value)
{
	if(l->next !=NULL)
	{
		if(l->value != value)
		{
			return find_element_by_value(l->next, value);
		}
		else
		{
			return l;
		}
	}
	return NULL;
}


/*
 * SYNOPSYS :
 *   int remove_element(list_elem_t * * ppl, int value)
 * DESCRIPTION :
 *   Removes from the list the first element with a value equal to the argument value and 
 *   frees the memory space reserved by this element.
 *   Attention : Depending on the position, the head of the list may need to be modified
 * PARAMETERS :
 *   list_elem_t ** ppl : pointer to the pointer of the head of the list
 *   int value  : value to be removed from the list
 * RESULT :
 *    0 in case of success
 *   -1 in case of error
 */



int remove_element(list_elem_t ** ppl, int value) 
{
	list_elem_t* l = *ppl;
	if(l != NULL)
	{
          if (l->next == NULL && l->value == value)
		{
               *ppl = NULL;

			return 0;
		}
		if(l->next->value != value)
		{
			return remove_element(&l->next, value);
		}
		else if(l->value == value)
		{
			ppl = &l->next;
			free_element(*ppl);
			return 0;
		}		
		else
		{
			l->next = l->next->next;
			return 0;
		}
	}
	return -1;
}

/*
 * SYNOPSYS :
 *   void reverse_list(list_elem_t * * l)
 * DESCRIPTION :
 *   Modifies the list by inversing the order of the elements.
 *   So the 1st element becomes the last element, the 2nd becomes the before last element, etc.)
 * PARAMETRES :
 *   list_elem_t ** l : pointer to the pointer of the head of the list
 * RESULTAT :
 *   aucun 
 */
void reverse_list(list_elem_t ** l) 
{	
	list_elem_t* previous = NULL;
	list_elem_t* current = *l;
	list_elem_t* next = NULL;

	while (current != NULL)
	{
 		next = current->next;
		current->next = previous;
		previous = current;
		current = next;
	}

	l = &previous;

	
}
