import React, { useState } from 'react';
import { Pagination } from 'react-bootstrap';


let Paginator = ({ totalItemsCount, pageSize, currentPage, onPageChanged, portionSize = 1 }) => {
    let pagesCount = Math.ceil(totalItemsCount / pageSize);
    let pages = [];
    let activePageIndex = currentPage;
    for (let index = 1; index <= pagesCount; index++) {
        pages.push(
            <Pagination.Item
                onClick={() => {
                    onPageChanged(index);
                }}
                key={index}
                active={index === activePageIndex}>
                {index}
            </Pagination.Item>
        );
    }
    
    let portionCount = Math.ceil(pagesCount / portionSize);
    let [portionNumber, setPortionNumber] = useState(1);
    let leftPortionPageNumber = (portionNumber - 1) * portionSize + 1;
    let rightPortionPageNumber = portionNumber * portionSize;

    return <div>
        <Pagination>
            {portionNumber > 1 && <Pagination.Prev onClick={() => { setPortionNumber(portionNumber - 1) }} />}
            {
                pages
                    .filter(page => page.key >= leftPortionPageNumber && page.key <= rightPortionPageNumber)
                    .map((page) => {
                        return page;
                    })
            }
            {portionCount > portionNumber && <Pagination.Next onClick={() => { setPortionNumber(portionNumber + 1) }} />}
        </Pagination>
    </div>
}

export default Paginator;