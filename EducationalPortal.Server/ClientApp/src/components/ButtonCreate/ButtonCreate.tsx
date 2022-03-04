import React, {FC} from 'react';
import {Button} from 'antd';

type Props = {
    loading?: boolean | undefined,
    isSubmit?: boolean | undefined,
};

export const ButtonCreate: FC<Props> = ({loading, isSubmit, children}) => {
    return (
        <Button loading={loading} type={'primary'} htmlType={!!isSubmit ? 'submit' : 'button'}>{children || 'Створити'}</Button>
    );
};
