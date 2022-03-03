import React, {FC} from 'react';
import {Link} from 'react-router-dom';
import {Button, Result} from 'antd';

export const Error: FC = () => {
    return (
        <Result
            status="404"
            title="404"
            subTitle="Сторінки не існує."
            extra={
                <Link to={'/'}>
                    <Button type="primary">На гловну</Button>
                </Link>
            }
        />
    );
};
